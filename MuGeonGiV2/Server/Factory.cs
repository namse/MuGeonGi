using MuGeonGiV2.Core;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class Factory : NancyModule
    {
        public Factory()
        {
            Options["*"] = _ =>
            {
                return new Response();
            };
            Post["/{InstrumentTypeName}"] = parameters =>
            {
                var aseembly = typeof(Instrument).Assembly;
                var type = aseembly.GetType($"MuGeonGiV2.Core.{parameters.InstrumentTypeName}");
                if (type == null)
                {
                    return new NotFoundResponse();
                }
                var instrument = (Instrument)Activator.CreateInstance(type);
                return Response.AsJson(instrument);
            };
            
            Delete["/{uuid}"] = parameters =>
            {
                if (Instrument.TryGet(parameters.uuid, out Instrument instrument))
                {
                    instrument.Dispose();
                    return new Response
                    {
                        StatusCode = HttpStatusCode.OK,
                    };
                }
                return new Response
                {
                    StatusCode = HttpStatusCode.NotFound,
                };
            };
        }
    }
}
