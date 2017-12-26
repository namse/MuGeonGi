using MuGeonGiV2.Core;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Streams.Effects;
using CSCore.Streams.SampleConverter;
using Nancy.Bootstrapper;
using Newtonsoft.Json.Converters;
using Equalizer = MuGeonGiV2.Core.Equalizer;

namespace MuGeonGiV2
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            this.ContractResolver = new CamelCasePropertyNamesContractResolver();
            this.Formatting = Formatting.Indented;
        }
    }
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            container.Register<JsonSerializer, CustomJsonSerializer>();
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
            {
                ctx.Response
                    .WithHeader("Access-Control-Allow-Origin", "*")
                    .WithHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-Type, Authorization");
            });
            pipelines.AfterRequest += (ctx) =>
            {
                Console.WriteLine($@"{ctx.Request.Path} - {ctx.Response.StatusCode}");
            };
        }
    }
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
            //Test();
        }

        static void Test()
        {
            var soundIn = new WasapiCapture();
            soundIn.Initialize();
            var soundInSource = new RealTimeSoundInSource(soundIn);

            var soundOut = new WasapiOut();
            soundOut.Initialize(soundInSource);
            soundIn.Start();
            soundOut.Play();

            soundOut.Stopped += (s, e) =>
            {
                Console.WriteLine("I'm dead but not dead, P.P.A.P");
                Task.Run(() =>
                {
                    soundOut.Play();
                });
            };

            while (true)
            {
                Console.ReadLine();
            }
        }

        static ISampleSource CreateEqulizer(ISampleSource existedSource)
        {
            var equalizer = new CSCore.Streams.Effects.Equalizer(existedSource);
            return equalizer;
        }
        static void Jungrue()
        {
            //var soundIn = new WasapiCapture();
            //var soundInSource = new SoundInSource(soundIn);
            //var source = soundInSource
            //    .ToSampleSource()
            //    .AppendSource(CreateEqulizer, out var equalizer)
            //    .AppendSource(CreateEqulizer)
            //    .ToWaveSource();

            //// 여기서 Equalizer 어떻게 조종하지?
            //var equalizer = (CSCore.Streams.Effects.Equalizer) equalizer;
            //equalizer.SampleFilters[0].AverageGainDB = 5;
            //equalizer.SampleFilters[4].AverageGainDB = 5;
            //equalizer.SampleFilters[9].AverageGainDB = 5;

            //foreach (var sampleFilter in equalizer.SampleFilters)
            //{
            //    Console.WriteLine($"{sampleFilter.AverageFrequency} - {sampleFilter.AverageGainDB}");
            //}
        }

        static void OnlyServer()
        {
            HostConfiguration hostConfigs = new HostConfiguration();
            hostConfigs.UrlReservations.CreateAutomatically = true;
            var uri = new Uri("http://localhost:8080");
            var host = new NancyHost(hostConfigs, uri);
            Console.WriteLine("Running on http://localhost:8080");
            host.Start();

            while (true)
            {
                Console.ReadLine();
            }
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

            while (true)
            {
                Console.ReadLine();
            }
        }

        static void Filter()
        {
            var mic = new Mic();
            var lowpassFilter = new LowpassFilter();
            var equalizer = new Equalizer();
            var highpassFilter = new HighpassFilter();
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

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
