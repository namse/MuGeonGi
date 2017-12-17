using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class Mic : Instrument
    {
        private readonly WasapiCapture SoundIn = new WasapiCapture();
        private RealTimeSoundInSource SoundInSource;
        public Mic()
        {
            SoundIn.Initialize();
            SoundInSource = new RealTimeSoundInSource(SoundIn);
            var source = new SampleToPcm16(SoundInSource.ToSampleSource());

            OutputJack = new OutputJack(source);
        }

        public void SetDevice(string deviceTag)
        {
            var device = AvailableDevices.Find(availableDevice => availableDevice.ToString() == deviceTag);
            // TODO: device 바꾸면 연결된게 다 나갈라지 않나요?
            SoundIn.Device = device;
        }

        public List<MMDevice> AvailableDevices {
            get {
                using (var deviceEnumerator = new MMDeviceEnumerator())
                using (var deviceCollection = deviceEnumerator.EnumAudioEndpoints(DataFlow.Capture, DeviceState.Active))
                {
                    return deviceCollection.ToList();
                }
            }
        }

        public void TurnOn()
        {
            SoundIn.Start();
            SoundIn.DataAvailable += (s, e) =>
            {
                Console.WriteLine((SoundInSource.Length / SoundInSource.WaveFormat.BytesPerSecond));
            };
        }
    }
}
