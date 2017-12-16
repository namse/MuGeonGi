using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;
using CSCore.Codecs;

namespace MuGeonGiV2.Core
{
    public class AudioPlayer: Instrument
    {
        readonly FakeStream Stream = new FakeStream();
        private IWaveSource AudioSource;
        
        public AudioPlayer()
        {
            OutputJack = new OutputJack(Stream);
        }

        public void SetFile(string filePath)
        {
            AudioSource = CodecFactory.Instance.GetCodec(filePath);
            Stream.SetStream(AudioSource);
            AudioSource.Position = AudioSource.Length;
        }

        public void Play()
        {
            AudioSource.Position = 0;
        }
    }
}
