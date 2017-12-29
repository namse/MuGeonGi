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
        public static void Disconnect(this Jack jack, Cable cable)
        {
            cable.OnDisonnect();
            if (jack is OutputJack outputJack)
            {
                cable.OutputJack = null;
            }
            else
            {
                cable.InputJack = null;
            }
            jack.Cable = null;
        }
    }
    public class Cable : Instrument
    {
        public override ICircuitNode Next => InputJack;
        public override ICircuitNode Previous => OutputJack;
        public override bool IsEndPoint => false;

        public void OnConnect()
        {
            var previousEndpoint = this.FindPreviousEndPoint();
            var nextEndpoint = this.FindNextEndPoint();
            if (previousEndpoint == null || nextEndpoint == null)
            {
                return;
            }

            var soundInInstrument = previousEndpoint as Instrument;
            soundInInstrument.SetCircuitUp();
        }

        public void OnDisonnect()
        {
            var previousEndpoint = this.FindPreviousEndPoint();
            var nextEndpoint = this.FindNextEndPoint();
            if (previousEndpoint == null || nextEndpoint == null)
            {
                return;
            }

            var soundInInstrument = previousEndpoint as Instrument;
            soundInInstrument.SetCircuitDown();
        }
    }
}
