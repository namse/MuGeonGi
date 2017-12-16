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
                if (!Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                var speaker = (Speaker)instrument;
                var devices = speaker.AvailableDevices.Select(device => device.ToString());
                return Response.AsJson(devices);
            };
            Post["/device/{DeviceName}"] = parameters => 
            {
                if (!Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                var speaker = (Speaker)instrument;
                speaker.SetDevice((string)parameters.DeviceName);
                return new Response();
            };
        }
    }
}
