using CSCore.SoundOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class Speaker : IInstrument
    {
        public InputJack InputJack = new InputJack();
        private WasapiOut SoundOut = new WasapiOut();

        private System.Timers.Timer Timer;

        public void TurnOn()
        {
            SoundOut.Initialize(InputJack.FakeStream);
            SoundOut.Play();

            //Timer = new System.Timers.Timer
            //{
            //    Interval = 1000,
            //    AutoReset = true,
            //    Enabled = true,
            //};

            //// Hook up the Elapsed event for the timer. 
            //Timer.Elapsed += (s, e) =>
            //{

            //};

            // Have the timer fire repeated events (true is the default)
            
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
