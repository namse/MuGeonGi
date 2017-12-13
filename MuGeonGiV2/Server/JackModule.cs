using MuGeonGiV2.Core;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class JackModule : MyModule
    {
        public JackModule() : base("/jack/{Uuid}")
        {
            Post["/connectCable/{CableUuid}"] = parameters =>
            {
                if (Instrument.TryGet(parameters.CableUuid, out Instrument instrument))
                {
                    if (Jack.TryGet(parameters.Uuid, out Jack jack))
                    {
                        var cable = (Cable)instrument;
                        jack.Connect(cable);
                        return new Response();
                    }
                }
                return new NotFoundResponse();
            };
        }
    }
}
