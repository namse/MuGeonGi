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
                if (!Storable.TryGet(parameters.CableUuid, out Storable instrument))
                {
                    return new NotFoundResponse();
                }
                if (!Storable.TryGet(parameters.Uuid, out Jack jack))
                {
                    return new NotFoundResponse();
                }
                var cable = (Cable)instrument;
                jack.Connect(cable);
                return new Response();
            };
        }
    }
}
