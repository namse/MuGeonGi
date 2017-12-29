using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSCore;
using CSCore.Codecs;
using CSCore.Codecs.AAC;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;

namespace MuGeonGiV2.Core
{
    public class AudioPlayer: Instrument
    {
        private IWaveSource _audioSource;
        public override bool IsEndPoint => true;
        public override IWaveSource OutputSource => _audioSource;

        public AudioPlayer()
        {
            OutputJack = new OutputJack(this);
        }

        public void SetFile(string filePath)
        {
            _audioSource = CodecFactory.Instance.GetCodec(filePath);
            if (_audioSource.WaveFormat.Channels == 2)
            {
                _audioSource = _audioSource.ToMono();
            }
            _audioSource.Position = _audioSource.Length;

            SetCircuitUp();
        }

        public override void TurnOn()
        {
        }

        public void Play()
        {
            var soundOutInstrument = GetSoundOutEndPoint();
            _audioSource.Position = 0;
            soundOutInstrument.TurnOn();
        }
    }
}
