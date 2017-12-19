using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class FakeStream : Stream
    {
        private IWaveSource _realStream;

        public override void Dispose()
        {
            _realStream.Dispose();
            GC.SuppressFinalize(this);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_realStream != null)
            {
                var read = _realStream.Read(buffer, offset, count);
                if (read < count)
                {
                    Array.Clear(buffer, offset + read, count - read);
                }
                return count;
            }
            Array.Clear(buffer, offset, count);
            return count;
        }

        internal void SetStream(IWaveSource stream)
        {
            _realStream = stream;
        }
    }
}
