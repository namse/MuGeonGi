using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class FakeStream : IWaveSource
    {
        private IWaveSource RealStream;
        public bool CanSeek => false;

        public long Position { get => 0; set => throw new InvalidOperationException(); }

        public long Length => 0;

        public WaveFormat WaveFormat => new WaveFormat(44100, 32, 1);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            Console.WriteLine(count);
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
