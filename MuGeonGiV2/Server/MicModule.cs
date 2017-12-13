using MuGeonGiV2.Core;
using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class MicModule : MyModule
    {
        public MicModule() : base("/Mic/{uuid}")
        {
            Get["/devices"] = parameters => {
                if (Instrument.TryGet(parameters.uuid, out Instrument instrument))
                {
                    var mic = (Mic)instrument;
                    var devices = mic.AvailableDevices.Select(device => device.ToString());
                    return Response.AsJson(devices);
                }
                return new NotFoundResponse();
            };
            Post["/device/{deviceName}"] = parameters => {
                if (Instrument.TryGet(parameters.uuid, out Instrument instrument))
                {
                    var mic = (Mic)instrument;
                    mic.SetDevice((string)parameters.deviceName);
                    return new Response();
                }
                return new NotFoundResponse();
            };
        }
    }
}
