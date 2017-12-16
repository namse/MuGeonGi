using MuGeonGiV2.Core;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class MicModule : MyModule
    {
        public MicModule() : base("/Mic/{Uuid}")
        {
            Get["/devices"] = parameters => 
            {
                if (!Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                var mic = (Mic)instrument;
                var devices = mic.AvailableDevices.Select(device => device.ToString());
                return Response.AsJson(devices);
            };
            Post["/device/{DeviceName}"] = parameters => 
            {
                if (!Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                var mic = (Mic)instrument;
                mic.SetDevice((string)parameters.DeviceName);
                return new Response();
            };
        }
    }
}
