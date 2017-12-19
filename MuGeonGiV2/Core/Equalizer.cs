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
        private CSCore.Streams.Effects.Equalizer _myEqualizer;
        public Equalizer()
        {
            // TODO
            //WaveSource = InputJack.FakeStream
            //    .ToSampleSource()
            //    .AppendSource(CSCore.Streams.Effects.Equalizer.Create10BandEqualizer, out _myEqualizer)
            //    .ToWaveSource(16);
        }

        public void SetFilter(Frequencies frequency, double gain)
        {
            int index = (int)frequency;
            _myEqualizer.SampleFilters[index].AverageGainDB = gain;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return WaveSource.Read(buffer, offset, count);
        }
    }
}
