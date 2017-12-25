using MuGeonGiV2.Core;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class SpeakerModule : MyModule
    {
        public SpeakerModule() : base("/speaker/{Uuid}")
        {
            Get["/devices"] = parameters =>
            {
                if (!Storable.TryGet(parameters.Uuid, out Speaker speaker))
                {
                    return new NotFoundResponse();
                }
                var devices = speaker.AvailableDevices.Select(device => device.ToString());
                return Response.AsJson(devices);
            };
            Post["/device/{DeviceName}"] = parameters => 
            {
                if (!Storable.TryGet(parameters.Uuid, out Speaker speaker))
                {
                    return new NotFoundResponse();
                }
                speaker.SetDevice((string)parameters.DeviceName);
                return new Response();
            };
            Post["/volume/{volume}"] = parameters =>
            {
                if (!Storable.TryGet(parameters.Uuid, out Speaker speaker))
                {
                    return new NotFoundResponse();
                }
                speaker.SetVolume((float)parameters.volume);
                return new Response();
            };
        }
    }
}
