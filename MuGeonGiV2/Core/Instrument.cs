using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Instrument : Storable, ICircuitNode
    {
        [JsonProperty]
        public List<InputJack> InputJacks = new List<InputJack>();
        [JsonProperty]
        public  List<OutputJack> OutputJacks = new List<OutputJack>();

        public virtual List<ICircuitNode> Nexts => OutputJacks.Cast<ICircuitNode>().ToList();
        public virtual List<ICircuitNode> Previouses => InputJacks.Cast<ICircuitNode>().ToList();
        public abstract bool IsEndPoint { get; }
        
        public virtual IWaveSource OutputSource => throw new NotImplementedException();

        public virtual void TurnOn()
        {
            throw new NotImplementedException();
        }

        public virtual void TurnOff()
        {
            throw new NotImplementedException();
        }

        public virtual void Initialize(IWaveSource outputSource)
        {
            throw new NotImplementedException();
        }

        public virtual void Uninitialize()
        {
            throw new NotImplementedException();
        }

        public void SetCircuitUp()
        {
            void SetCircuitUp(ICircuitNode self, IWaveSource source)
            {
                Console.WriteLine($"{self.GetType()}");
                self.Nexts.ForEach((next) =>
                    {
                        if (next is Effector effector)
                        {
                            source = effector.AppendSource(source);
                        }
                        if (next.IsEndPoint)
                        {
                            var soundOutInstrument = next as Instrument;
                            soundOutInstrument.Initialize(source);
                            soundOutInstrument.TurnOn();
                            TurnOn();
                            return;
                        }
                        SetCircuitUp(next, source);
                    });
            }

            SetCircuitUp(this, OutputSource);
        }

        public void SetCircuitDown()
        {
            var nextEndPoints = this.FindNextEndPoints();
            nextEndPoints.ToList().ForEach((nextEndPoint) =>
            {
                var soundOutInstrument = nextEndPoint as Instrument;
                soundOutInstrument.TurnOff();
                soundOutInstrument.Uninitialize();
            });

            if (nextEndPoints.Count == 1)
            {
                TurnOff();
            }
        }
    }
}
