using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.Streams;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class Cable
    {
        public FakeStream FakeStream = new FakeStream();
        internal void PutSoundInSource(IWaveSource waveSource)
        {
            FakeStream.SetStream(waveSource);
        }
    }
}
