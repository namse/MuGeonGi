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
        private double _frequency = 1000;
        private int _sampleRate;
        private BiQuadFilterSource _biQuadFilterSource;

        public void SetFrequency(double frequency)
        {
            _frequency = frequency;
            if (_biQuadFilterSource != null)
            {
                InitializeFilter();
            }
        }

        public override IWaveSource AppendSource(IWaveSource source)
        {
            var newSource = source
                .ToSampleSource()
                .AppendSource(x => new BiQuadFilterSource(x), out _biQuadFilterSource)
                .ToWaveSource();

            _sampleRate = newSource.WaveFormat.SampleRate;

            InitializeFilter();

            return newSource;
        }

        private void InitializeFilter()
        {
            _biQuadFilterSource.Filter = new CSCore.DSP.HighpassFilter(_sampleRate, _frequency);
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
