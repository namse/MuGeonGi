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
            
            Delete["/instrument/{uuid}"] = parameters =>
            {
                if (!Storable.TryGet(parameters.uuid, out Instrument instrument))
                {
                    return new NotFoundResponse();
                }
                    
                instrument.Dispose();
                return new Response();
            };
        }
    }
}
