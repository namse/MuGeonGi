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
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var mic = (Mic)instrument;
                    var devices = mic.AvailableDevices.Select(device => device.ToString());
                    return Response.AsJson(devices);
                }
                return new NotFoundResponse();
            };
            Post["/device/{DeviceName}"] = parameters => 
            {
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var mic = (Mic)instrument;
                    mic.SetDevice((string)parameters.DeviceName);
                    return new Response();
                }
                return new NotFoundResponse();
            };
            Post["/{MethodName}"] = parameters =>
            {
                if (Instrument.TryGet(parameters.Uuid, out Instrument instrument))
                {
                    var mic = (Mic)instrument;
                    var type = typeof(Mic);
                    MethodInfo method = type.GetMethod(parameters.MethodName);
                    method.Invoke(mic, null);
                    return new Response();
                }
                return new NotFoundResponse();
            };
        }
    }
}
