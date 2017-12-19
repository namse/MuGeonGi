using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class LowpassFilter: Effector
    {
        public IWaveSource WaveSource;
        public LowpassFilter(double frequency)
        {
            // TODO
            //WaveSource = InputJack.FakeStream
            //    .ToSampleSource()
            //    .AppendSource(x => new BiQuadFilterSource(x), out var biQuadFilterSource)
            //    .ToWaveSource(16);
            //biQuadFilterSource.Filter = new CSCore.DSP.LowpassFilter(InputJack.FakeStream.WaveFormat.SampleRate, frequency);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return WaveSource.Read(buffer, offset, count);
        }
    }
}
