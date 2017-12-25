﻿using CSCore;
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
        public IWaveSource WaveSource;
        public HighpassFilter(): this(1000) {
        }

        public HighpassFilter(double frequency)
        {
            // TODO
            //WaveSource = InputJack.FakeStream
            //    .ToSampleSource()
            //    .AppendSource(x => new BiQuadFilterSource(x), out var biQuadFilterSource)
            //    .ToWaveSource(16);
            //biQuadFilterSource.Filter = new CSCore.DSP.HighpassFilter(InputJack.FakeStream.WaveFormat.SampleRate, frequency);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return WaveSource.Read(buffer, offset, count);
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
