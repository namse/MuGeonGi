using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class Speaker : IInstrument
    {
        public InputJack InputJack = new InputJack();
        private WasapiOut SoundOut = new WasapiOut();
        

        public void TurnOn()
        {
            SoundOut.Initialize(InputJack.FakeStream);
            SoundOut.Play();
            SoundOut.Stopped += (s, e) =>
            {
                Console.WriteLine("HI");
            };
        }
    }
}
