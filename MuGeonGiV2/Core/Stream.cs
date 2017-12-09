using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public abstract class Stream : IWaveSource
    {
        public bool CanSeek => false;

        public long Position { get => 0; set => throw new InvalidOperationException(); }

        public long Length => 0;

        public WaveFormat WaveFormat => new WaveFormat(44100, 32, 1);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public abstract int Read(byte[] buffer, int offset, int count);
    }
}
