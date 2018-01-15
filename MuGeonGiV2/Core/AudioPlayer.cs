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
        private DateTime _backgroundPlayStartTime;
        private bool _isPlayingOnBackground;

        public AudioPlayer()
        {
            OutputJacks.Add(new OutputJack(this));
        }

        public void SetFile(string filePath)
        {
            _isPlayingOnBackground = false;

            if (this.FindNextEndPoints().Count > 0)
            {
                SetCircuitDown();
            }
            
            _audioSource = CodecFactory.Instance.GetCodec(filePath);
            if (_audioSource.WaveFormat.Channels == 2)
            {
                _audioSource = _audioSource.ToMono();
            }
            _audioSource.Position = _audioSource.Length;

            if (this.FindNextEndPoints().Count > 0)
            {
                SetCircuitUp();
            }
        }

        public override void TurnOn()
        {
            if (_isPlayingOnBackground)
            {
                var deltaTime = DateTime.Now - _backgroundPlayStartTime;
                var deltaTimeByte = _audioSource.WaveFormat.BytesPerSecond * (long)deltaTime.TotalSeconds;
                _audioSource.Position += deltaTimeByte;
                _isPlayingOnBackground = false;
            }
        }

        public void Play()
        {
            _audioSource.Position = 0;
            this.FindNextEndPoints().ToList().ForEach((nextEndPoint) =>
            {
                var soundOutInstrument = nextEndPoint as Instrument;
                soundOutInstrument.TurnOn();
            });
        }

        public override void TurnOff()
        {
            _isPlayingOnBackground = true;
            _backgroundPlayStartTime = DateTime.Now;
        }
    }
}
