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
        private readonly WasapiOut _soundOut = new WasapiOut();
        public override bool IsEndPoint => true;

        public Speaker()
        {
            InputJack = new InputJack(this);
        }

        internal void SetDevice(string deviceTag)
        {
            var device = AvailableDevices.Find(availableDevice => availableDevice.ToString() == deviceTag);
            // TODO: device 바꾸면 연결된게 다 나갈라지 않나요?
            _soundOut.Device = device;
        }

        public void SetVolume(float volume)
        {
            _soundOut.Volume = volume;
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
        }

        public override void TurnOn()
        {
            _soundOut.Play();
            _soundOut.Stopped += (s, e) =>
            {
                Console.WriteLine("I'm dead but not dead, P.P.A.P");
                Task.Run(() =>
                {
                    _soundOut.Play();
                });
            };
        }
    }
}
