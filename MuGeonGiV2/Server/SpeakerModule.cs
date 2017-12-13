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
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var speaker = (Speaker)instrument;
                    var devices = speaker.AvailableDevices.Select(device => device.ToString());
                    return Response.AsJson(devices);
                }
                return new NotFoundResponse();
            };
            Post["/device/{DeviceName}"] = parameters => 
            {
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var speaker = (Speaker)instrument;
                    speaker.SetDevice((string)parameters.DeviceName);
                    return new Response();
                }
                return new NotFoundResponse();
            };
            Post["/{MethodName}"] = parameters =>
            {
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var speaker = (Speaker)instrument;
                    var type = typeof(Speaker);
                    MethodInfo method = type.GetMethod(parameters.MethodName);
                    method.Invoke(speaker, null);
                    return new Response();
                }
                return new NotFoundResponse();
            };
        }
    }
}
