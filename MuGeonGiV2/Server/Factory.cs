using MuGeonGiV2.Core;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class Factory : MyModule
    {
        Dictionary<string, Func<Instrument>> ConstructorDictionary = new Dictionary<string, Func<Instrument>>();

        public Factory()
        {
            ConstructorDictionary.Add("mic", () => new Mic());
            ConstructorDictionary.Add("speaker", () => new Speaker());
            ConstructorDictionary.Add("cable", () => new Cable());
            foreach (var name in ConstructorDictionary.Keys)
            {
                var constructor = ConstructorDictionary[name];
                Post[$"/{name}"] = _ => {
                    var instrument = constructor();
                    return Response.AsJson(instrument);
                };
            }
            
            Delete["/{uuid}"] = parameters => {
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

    public abstract class MyModule : NancyModule
    {
        protected MyModule(string modulePath = ""): base(modulePath)
        {
            After.AddItemToEndOfPipeline((ctx) => ctx.Response
            .WithHeader("Access-Control-Allow-Origin", "*")
            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));
        }
    }
}
