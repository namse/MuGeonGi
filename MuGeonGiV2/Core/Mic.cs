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
    public class Mic : IInstrument
    {
        private WasapiCapture SoundIn = new WasapiCapture();
        public OutputJack OutputJack;

        public Mic()
        {
            SoundIn.Initialize();
            var soundInSource = new SoundInSource(SoundIn);
            var pcm32source = new SampleToPcm32(soundInSource.ToSampleSource());

            OutputJack = new OutputJack(pcm32source);
        }

        public void TurnOn()
        {
            SoundIn.Start();
        }
    }
}
