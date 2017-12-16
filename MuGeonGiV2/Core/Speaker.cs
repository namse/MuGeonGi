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
        private readonly WasapiOut SoundOut = new WasapiOut();

        public Speaker()
        {
            InputJack = new InputJack();
        }

        internal void SetDevice(string deviceTag)
        {
            var device = AvailableDevices.Find(availableDevice => availableDevice.ToString() == deviceTag);
            // TODO: device 바꾸면 연결된게 다 나갈라지 않나요?
            SoundOut.Device = device;
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

        public void TurnOn()
        {
            SoundOut.Initialize(InputJack.FakeStream);
            SoundOut.Play();

            SoundOut.Stopped += (s, e) =>
            {
                Console.WriteLine("I'm dead but not dead, P.P.A.P");
                Task.Run(() =>
                {
                    SoundOut.Play();
                });
            };
        }
    }
}
