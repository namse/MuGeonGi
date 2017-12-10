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
        private static Dictionary<string, IInstrument> InstrumentDictionary = new Dictionary<string, IInstrument>();
        Dictionary<string, Func<IInstrument>> ConstructorDictionary = new Dictionary<string, Func<IInstrument>>();

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
                    var uuid = UUID();
                    InstrumentDictionary.Add(uuid, instrument);
                    return uuid;
                };
            }
            
            Delete["/{uuid}"] = parameters => {
                if (InstrumentDictionary.TryGetValue(parameters.uuid, out IInstrument instrument))
                {
                    instrument.Destroy();
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

        private string UUID() => System.Guid.NewGuid().ToString();
        public static bool TryGetInstrument(string uuid, out IInstrument instrument) =>
            InstrumentDictionary.TryGetValue(uuid, out instrument);
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
