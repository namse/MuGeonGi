using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class Equalizer: Effector
    {
        public enum Frequencies : int
        {
            Hz31 = 0,
            Hz62,
            Hz125,
            Hz250,
            Hz500,
            Hz1000,
            Hz2000,
            Hz4000,
            Hz8000,
            Hz16000,
        }

        public IWaveSource WaveSource;
        private CSCore.Streams.Effects.Equalizer _equalizer;

        public void SetFilter(Frequencies frequency, double gain)
        {
            var index = (int)frequency;
            _equalizer.SampleFilters[index].AverageGainDB = gain;
        }

        public override IWaveSource AppendSource(IWaveSource source)
        {
            return source
                .ToSampleSource()
                .AppendSource(CSCore.Streams.Effects.Equalizer.Create10BandEqualizer, out _equalizer)
                .ToWaveSource();
        }
    }
}
