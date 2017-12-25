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
        public InputJack InputJack;
        [JsonProperty]
        public  OutputJack OutputJack;

        public virtual ICircuitNode Next => OutputJack;
        public virtual ICircuitNode Previous => InputJack;
        public abstract bool IsEndPoint { get; }
        public virtual IWaveSource OutputSource => throw new NotImplementedException();

        public virtual void TurnOn()
        {
            throw new NotImplementedException();
        }

        public virtual void Initialize(IWaveSource outputSource)
        {
            throw new NotImplementedException();
        }

        public void SetCircuitUp()
        {
            var next = Next;
            var source = OutputSource;
            while (next.IsEndPoint == false)
            {
                if (next is Effector)
                {
                    var effector = next as Effector;
                    source = effector.AppendSource(source);
                }
                next = next.Next;
            }
            var soundOutInstrument = next as Instrument;
            soundOutInstrument.Initialize(source);
            soundOutInstrument.TurnOn();
            TurnOn();
        }

        protected Instrument GetSoundOutEndPoint()
        {
            var next = Next;
            while (next.IsEndPoint == false)
            {
                next = next.Next;
            }
            return next as Instrument;
        }
    }
}
