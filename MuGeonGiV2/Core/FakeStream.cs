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
        private IWaveSource RealStream;

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (RealStream != null)
            {
                return RealStream.Read(buffer, offset, count);
            }
            else
            {
                Array.Clear(buffer, offset, count);
                return count;
            }
        }

        internal void SetStream(IWaveSource stream)
        {
            RealStream = stream;
        }
    }
}
