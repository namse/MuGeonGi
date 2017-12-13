using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.SoundIn;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class OutputJack : Jack
    {
        private WasapiCapture SoundIn;
        private IWaveSource WaveSource;

        public OutputJack(IWaveSource source)
        {
            WaveSource = source;
        }

        public override void Connect(Cable cable)
        {
            cable.PutSoundInSource(WaveSource);
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
