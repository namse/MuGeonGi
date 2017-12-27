using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSCore.Streams.Effects;
using MuGeonGiV2.Core;
using Nancy;
using Nancy.Extensions;
using Newtonsoft.Json.Linq;

namespace MuGeonGiV2.Server
{
    public class AudioPlayerModule : NancyModule
    {
        public AudioPlayerModule() : base("/audioplayer/{Uuid}")
        {
            Post["/upload"] = parameters =>
            {
                if (!Storable.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                var audioPlayer = (AudioPlayer)instrument;

                var file = Request.Files.ToList()[0];
                var filename = file.Name;
                var inputStream = file.Value;
                var fileStream = File.Create($"./{filename}");
                inputStream.CopyTo(fileStream);
                fileStream.Close();

                audioPlayer.SetFile($"./{filename}");
                return new Response();
            };
            Post["/play"] = parameters =>
            {
                if (!Storable.TryGet(parameters.Uuid, out AudioPlayer audioPlayer))
                {
                    return new NotFoundResponse();
                }
                audioPlayer.Play(); 
                return new Response();
            };
        }
    }
}
