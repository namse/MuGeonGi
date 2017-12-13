using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Storable<T> : IDisposable
    {
        private static readonly Dictionary<string, T> Dictionary = new Dictionary<string, T>();

        [JsonProperty]
        public readonly string Uuid = Guid.NewGuid().ToString();

        ~Storable()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dictionary.Remove(Uuid);
            GC.SuppressFinalize(this);
        }

        public static bool TryGet(string uuid, out T instrument)
        {
            return Dictionary.TryGetValue(uuid, out instrument);
        }
    }
}
