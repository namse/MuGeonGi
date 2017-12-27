using MuGeonGiV2.Core;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Server
{
    public class JackModule : NancyModule
    {
        public JackModule() : base("/jack/{Uuid}")
        {
            Post["/connectCable/{CableUuid}"] = parameters =>
            {
                if (!Storable.TryGet(parameters.CableUuid, out Cable cable))
                {
                    return new NotFoundResponse();
                }
                if (!Storable.TryGet(parameters.Uuid, out Jack jack))
                {
                    return new NotFoundResponse();
                }
                jack.Connect(cable);
                return new Response();
            };
            Post["/disconnectCable/{CableUuid}"] = parameters =>
            {
                if (!Storable.TryGet(parameters.CableUuid, out Cable cable))
                {
                    return new NotFoundResponse();
                }
                if (!Storable.TryGet(parameters.Uuid, out Jack jack))
                {
                    return new NotFoundResponse();
                }
                jack.Disconnect(cable);
                return new Response();
            };
        }
    }
}
