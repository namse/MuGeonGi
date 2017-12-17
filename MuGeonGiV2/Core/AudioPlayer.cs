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
using EventHook;
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
        private readonly EditableStream Stream;
        private IWaveSource AudioSource;
        private KeyBindingInfo KeyBindingInfo;
        public AudioPlayer()
        {
            Stream = new EditableStream(Read);
            OutputJack = new OutputJack(Stream);
            KeyboardHook.Hook.KeyDownEvent += KeyDown;
        }

        public void SetFile(string filePath)
        {
            AudioSource = CodecFactory.Instance.GetCodec(filePath)
                .ChangeSampleRate(44100)
                .ToSampleSource()
                .ToWaveSource(16);
            if (AudioSource.WaveFormat.Channels == 2)
            {
                AudioSource = AudioSource.ToMono();
            }
            AudioSource.Position = AudioSource.Length;
        }

        public void Play()
        {
            AudioSource.Position = 0;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (AudioSource != null)
            {
                var read = AudioSource.Read(buffer, offset, count);
                var availableLength = (AudioSource.Length - AudioSource.Position);
                // Console.WriteLine($"{count}-{read}-{availableLength}");
                if (availableLength <= 0)
                {
                    AudioSource.Position = AudioSource.Length;
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
            KeyBindingInfo = new KeyBindingInfo()
            {
                ShiftKey = shiftKey,
                CtrlKey = ctrlKey,
                AltKey = altKey,
                Key = key,
            };
        }

        private void KeyDown(KeyboardHookEventArgs e)
        {
            if (KeyBindingInfo?.ShiftKey != e.isShiftPressed
                || KeyBindingInfo.CtrlKey != e.isCtrlPressed
                || KeyBindingInfo.AltKey != e.isAltPressed
                || KeyBindingInfo.Key != e.Key)
            {
                return;
            }
            Play();
        }
    }
}
