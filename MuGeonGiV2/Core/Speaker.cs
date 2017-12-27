using CSCore.CoreAudioAPI;
using CSCore.SoundOut;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSCore;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Speaker : Instrument
    {
        private WasapiOut _soundOut = new WasapiOut();
        private float _volume = 1; // default by library (CSCore)
        private bool _isInitialized = false;
        public override bool IsEndPoint => true;

        public Speaker()
        {
            InputJack = new InputJack(this);
            
            _soundOut.Stopped += (s, e) =>
            {
                Console.WriteLine("I'm dead but not dead, P.P.A.P");
            };
        }

        internal void SetDevice(string deviceTag)
        {
            var device = AvailableDevices.Find(availableDevice => availableDevice.ToString() == deviceTag);
            // TODO: device 바꾸면 연결된게 다 나갈라지 않나요?
            _soundOut.Device = device;
        }

        public void SetVolume(float volume)
        {
            _volume = volume;
            if (_isInitialized)
            {
                _soundOut.Volume = volume;
            }
        }

        public List<MMDevice> AvailableDevices
        {
            get
            {
                using (var deviceEnumerator = new MMDeviceEnumerator())
                using (var deviceCollection = deviceEnumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active))
                {
                    return deviceCollection.ToList();
                }
            }
        }

        public override void Initialize(IWaveSource source)
        {
            _soundOut.Initialize(source);
            _isInitialized = true;
            SetVolume(_volume);
        }

        public override void Uninitialize()
        {
            _isInitialized = false;
        }

        public override void TurnOn()
        {
            if (_isInitialized)
            {
                _soundOut.Play();
            }   
        }

        public override void TurnOff()
        {
            _soundOut.Stop();
        }
    }
}
