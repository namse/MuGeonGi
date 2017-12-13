using MuGeonGiV2.Core;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuGeonGiV2
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // MicToSpeaker();
            // Filter();
            OnlyServer();
        }

        static void OnlyServer()
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            var uri = new Uri("http://localhost:8080");
            var host = new NancyHost(hostConfigs, uri);
            Console.WriteLine("Running on http://localhost:8080");
            host.Start();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void MicToSpeaker()
        {
            var mic = new Mic();
            var speaker = new Speaker();

            var cable1 = new Cable();

            mic.OutputJack.Connect(cable1);
            speaker.InputJack.Connect(cable1);

            mic.TurnOn();
            speaker.TurnOn();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void Filter()
        {
            var mic = new Mic();
            var lowpassFilter = new LowpassFilter(1000);
            var equalizer = new Equalizer();
            var highpassFilter = new HighpassFilter(1000);
            var speaker = new Speaker();

            var cable1 = new Cable();
            var cable2 = new Cable();
            var cable3 = new Cable();
            var cable4 = new Cable();

            mic.OutputJack.Connect(cable1);
            lowpassFilter.InputJack.Connect(cable1);

            lowpassFilter.OutputJack.Connect(cable2);
            equalizer.InputJack.Connect(cable2);

            equalizer.OutputJack.Connect(cable3);
            highpassFilter.InputJack.Connect(cable3);

            highpassFilter.OutputJack.Connect(cable4);
            speaker.InputJack.Connect(cable4);

            mic.TurnOn();
            speaker.TurnOn();

            foreach (Equalizer.Frequencies frequency in Enum.GetValues(typeof(Equalizer.Frequencies)))
            {
                equalizer.SetFilter(frequency, 16);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
