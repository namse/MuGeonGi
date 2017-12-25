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
        private IWaveSource _audioSource;
        private KeyBindingInfo _keyBindingInfo;
        public override bool IsEndPoint => true;
        public override IWaveSource OutputSource => _audioSource;

        public AudioPlayer()
        {
            OutputJack = new OutputJack(this);
            KeyboardHook.Hook.KeyDownEvent += KeyDown;
        }

        public void SetFile(string filePath)
        {
            _audioSource = CodecFactory.Instance.GetCodec(filePath);
            if (_audioSource.WaveFormat.Channels == 2)
            {
                _audioSource = _audioSource.ToMono();
            }
            _audioSource.Position = _audioSource.Length;
        }

        public override void TurnOn()
        {
            _audioSource.Position = 0;
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
            TurnOn();
        }
    }
}
