using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.Streams;
using CSCore;

namespace MuGeonGiV2.Core
{
    public static class CableExtension {
        public static void Connect(this Jack jack, Cable cable)
        {
            if (jack is OutputJack outputJack)
            {
                cable.OutputJacks = new List<OutputJack>(){ outputJack };
            }
            else
            {

                cable.InputJacks = new List<InputJack>() { (InputJack)jack };
            }
            jack.Cable = cable;
            cable.OnConnect();
        }
        public static void Disconnect(this Jack jack, Cable cable)
        {
            cable.OnDisonnect();
            if (jack is OutputJack outputJack)
            {
                cable.OutputJacks = new List<OutputJack>();
            }
            else
            {
                cable.InputJacks = new List<InputJack>();
            }
            jack.Cable = null;
        }
    }
    public class Cable : Instrument
    {
        public override bool IsEndPoint => false;
        public override List<ICircuitNode> Nexts => InputJacks.Cast<ICircuitNode>().ToList();
        public override List<ICircuitNode> Previouses => OutputJacks.Cast<ICircuitNode>().ToList();

        public Cable()
        {
            OutputJacks.Add(new OutputJack(this));
            InputJacks.Add(new InputJack(this));
        }

        public void OnConnect()
        {
            var previousEndpoints = this.FindPreviousEndPoints();
            var nextEndpoints = this.FindNextEndPoints();
            if (previousEndpoints.Count == 0 || nextEndpoints.Count == 0)
            {
                return;
            }

            previousEndpoints.ForEach((previousEndpoint) =>
            {
                var soundInInstrument = previousEndpoint as Instrument;
                soundInInstrument.SetCircuitUp();
            });
        }

        public void OnDisonnect()
        {
            var previousEndpoints = this.FindPreviousEndPoints();
            var nextEndpoints = this.FindNextEndPoints();
            if (previousEndpoints.Count == 0 || nextEndpoints.Count == 0)
            {
                return;
            }

            previousEndpoints.ForEach((previousEndpoint) =>
            {
                var soundInInstrument = previousEndpoint as Instrument;
                soundInInstrument.SetCircuitDown();
            });
        }
    }
}
