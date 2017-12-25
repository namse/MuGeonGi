using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class Instrument : Storable, ICircuitNode
    {
        [JsonProperty]
        public InputJack InputJack;
        [JsonProperty]
        public  OutputJack OutputJack;

        public virtual ICircuitNode Next => OutputJack;
        public virtual ICircuitNode Previous => InputJack;
        public abstract bool IsEndPoint { get; }
        public virtual IWaveSource OutputSource => throw new NotImplementedException();

        public virtual void TurnOn()
        {
            throw new NotImplementedException();
        }

        public virtual void Initialize(IWaveSource outputSource)
        {
            throw new NotImplementedException();
        }
    }
}
