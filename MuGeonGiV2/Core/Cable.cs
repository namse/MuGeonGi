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
                cable.OutputJack = outputJack;
            }
            else
            {
                cable.InputJack = (InputJack)jack;
            }
            jack.Cable = cable;
            cable.OnConnect();
        }
    }
    public class Cable : Instrument
    {
        public override ICircuitNode Next => InputJack;
        public override ICircuitNode Previous => OutputJack;
        public override bool IsEndPoint => false;

        internal void OnConnect()
        {
            if (Previous == null || Next == null)
            {
                return;
            }
            var previousEndpoint = Previous.FindEndPoint(this);
            var nextEndpoint = Next.FindEndPoint(this);
            if (previousEndpoint == null || nextEndpoint == null)
            {
                return;
            }

            // TODO : BEGIN Intialize, Start, Play
            var soundOutInstrument = (Instrument) nextEndpoint;
            var soundInInstrument = (Instrument) previousEndpoint;

            soundOutInstrument.Initialize(soundInInstrument.OutputSource);
            soundOutInstrument.TurnOn();
            soundInInstrument.TurnOn();
        }
    }
}
