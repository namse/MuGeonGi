using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.Streams;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class Cable : IInstrument
    {
        public FakeStream FakeStream = new FakeStream();

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        internal void PutSoundInSource(IWaveSource waveSource)
        {
            FakeStream.SetStream(waveSource);
        }
    }
}
