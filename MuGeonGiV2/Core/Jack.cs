using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Jack : Storable, ICircuitNode
    {
        protected Instrument Instrument;
        public Cable Cable;

        public abstract ICircuitNode Next { get; }
        public abstract ICircuitNode Previous { get; }
        public bool IsEndPoint => false;

        protected Jack(Instrument instrument)
        {
            Instrument = instrument;
        }
    }
}
