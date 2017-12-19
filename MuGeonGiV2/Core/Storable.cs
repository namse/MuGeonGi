using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Storable : IDisposable
    {
        public static readonly Dictionary<string, Storable> Dictionary = new Dictionary<string, Storable>();

        [JsonProperty]
        public readonly string Uuid = Guid.NewGuid().ToString();

        public Storable()
        {
            Dictionary.Add(Uuid, this);
        }

        ~Storable()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dictionary.Remove(Uuid);
            GC.SuppressFinalize(this);
        }
        public static bool TryGet<TActual>(string key, out TActual value) where TActual : Storable
        {
            if (Dictionary.TryGetValue(key, out var tmp))
            {
                value = (TActual)tmp;
                return true;
            }
            value = default(TActual);
            return false;
        }
    }
}
