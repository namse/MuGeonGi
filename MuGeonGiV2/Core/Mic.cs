using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
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
            Console.WriteLine(SoundIn.WaveFormat);
            var soundInSource = new SoundInSource(SoundIn);
            Console.WriteLine(soundInSource.ToSampleSource().WaveFormat); 

            OutputJack = new OutputJack(SoundIn);
        }

        public void TurnOn()
        {
            SoundIn.Start();
        }
    }
}
