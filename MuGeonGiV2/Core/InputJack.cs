using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class InputJack : Jack
    {
        private readonly IWaveSource _waveSource;

        public override ICircuitNode Next => Instrument;
        public override ICircuitNode Previous => Cable;

        public InputJack(Instrument instrument) : base(instrument)
        {
        }
    }
}
