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
            HZ_31 = 0,
            HZ_62,
            HZ_125,
            HZ_250,
            HZ_500,
            HZ_1000,
            HZ_2000,
            HZ_4000,
            HZ_8000,
            HZ_16000,
        }

        public IWaveSource WaveSource;
        private CSCore.Streams.Effects.Equalizer MyEqualizer;
        public Equalizer()
        {
            WaveSource = InputJack.FakeStream
                .ToSampleSource()
                .AppendSource(CSCore.Streams.Effects.Equalizer.Create10BandEqualizer, out MyEqualizer)
                .ToWaveSource(24);
        }

        public void SetFilter(Frequencies frequency, double gain)
        {
            int index = (int)frequency;
            MyEqualizer.SampleFilters[index].AverageGainDB = gain;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return WaveSource.Read(buffer, offset, count);
        }
    }
}
