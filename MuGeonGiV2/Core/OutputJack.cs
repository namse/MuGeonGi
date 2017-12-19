using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.SoundIn;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class OutputJack : Jack
    {
        private readonly IWaveSource _waveSource;

        public override ICircuitNode Next => Cable;
        public override ICircuitNode Previous => Instrument;

        public OutputJack(Instrument instrument) : base(instrument)
        {
        }
    }
}
