using CSCore;
using CSCore.DSP;
using CSCore.Streams.SampleConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class HighpassFilter: Effector
    {
        public override IWaveSource AppendSource(IWaveSource source)
        {
            var newSource = source
                .ToSampleSource()
                .AppendSource(x => new BiQuadFilterSource(x), out var biQuadFilterSource)
                .ToWaveSource();
            biQuadFilterSource.Filter = new CSCore.DSP.HighpassFilter(newSource.WaveFormat.SampleRate, 1000);

            return newSource;
        }
    }
    public class BiQuadFilterSource : SampleAggregatorBase
    {
        private readonly object _lockObject = new object();
        private BiQuad _biquad;

        public BiQuad Filter
        {
            get => _biquad;
            set
            {
                lock (_lockObject)
                {
                    _biquad = value;
                }
            }
        }

        public BiQuadFilterSource(ISampleSource source) : base(source)
        {
        }

        public override int Read(float[] buffer, int offset, int count)
        {
            var read = base.Read(buffer, offset, count);
            lock (_lockObject)
            {
                if (Filter == null)
                {
                    return read;
                }
                for (var i = 0; i < read; i++)
                {
                    buffer[i + offset] = Filter.Process(buffer[i + offset]);
                }
            }

            return read;
        }
    }
}
