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
        public override IWaveSource AppendSource(IWaveSource source)
        {
            var newSource = source
                .ToSampleSource()
                .AppendSource(x => new BiQuadFilterSource(x), out var biQuadFilterSource)
                .ToWaveSource();
            biQuadFilterSource.Filter = new CSCore.DSP.LowpassFilter(newSource.WaveFormat.SampleRate, 1000);

            return newSource;
        }
    }
}
