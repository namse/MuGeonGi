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
using FMUtils.KeyboardHook;

namespace MuGeonGiV2.Core
{
    public class KeyBindingInfo
    {
        public bool ShiftKey;
        public bool CtrlKey;
        public bool AltKey;
        public Keys Key;
    }
    public class AudioPlayer: Instrument
    {
        private readonly EditableStream _stream;
        private IWaveSource _audioSource;
        private KeyBindingInfo _keyBindingInfo;
        public override bool IsEndPoint => true;
        public AudioPlayer()
        {
            _stream = new EditableStream(Read);
            OutputJack = new OutputJack(this);
            KeyboardHook.Hook.KeyDownEvent += KeyDown;
        }

        public void SetFile(string filePath)
        {
            _audioSource = CodecFactory.Instance.GetCodec(filePath)
                .ChangeSampleRate(44100)
                .ToSampleSource()
                .ToWaveSource(16);
            if (_audioSource.WaveFormat.Channels == 2)
            {
                _audioSource = _audioSource.ToMono();
            }
            _audioSource.Position = _audioSource.Length;
        }

        public void Play()
        {
            _audioSource.Position = 0;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (_audioSource != null)
            {
                var read = _audioSource.Read(buffer, offset, count);
                var availableLength = (_audioSource.Length - _audioSource.Position);
                // Console.WriteLine($"{count}-{read}-{availableLength}");
                if (availableLength <= 0)
                {
                    _audioSource.Position = _audioSource.Length;
                    Array.Clear(buffer, offset + read + (int)availableLength, count - read);
                    return count;
                }
                if (read != 0)
                {
                    return read;
                }
                if (availableLength > count)
                {
                    return 0;
                }
            }
            Array.Clear(buffer, offset, count);
            return count;
        }

        public void BindKey(bool shiftKey, bool ctrlKey, bool altKey, Keys key)
        {
            _keyBindingInfo = new KeyBindingInfo()
            {
                ShiftKey = shiftKey,
                CtrlKey = ctrlKey,
                AltKey = altKey,
                Key = key,
            };
        }

        private void KeyDown(KeyboardHookEventArgs e)
        {
            if (_keyBindingInfo?.ShiftKey != e.isShiftPressed
                || _keyBindingInfo.CtrlKey != e.isCtrlPressed
                || _keyBindingInfo.AltKey != e.isAltPressed
                || _keyBindingInfo.Key != e.Key)
            {
                return;
            }
            Play();
        }
    }
}
