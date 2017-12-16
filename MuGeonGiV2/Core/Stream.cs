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
        public virtual bool CanSeek => false;

        public virtual long Position { get => 0; set => throw new InvalidOperationException(); }

        public virtual long Length => 0;

        public virtual WaveFormat WaveFormat => new WaveFormat(44100, 16, 1);

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public abstract int Read(byte[] buffer, int offset, int count);
    }
}
