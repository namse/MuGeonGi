using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public abstract class Effector : Instrument
    {
        protected EditableStream Stream;
        protected Effector()
        {
            Stream = new EditableStream(Read);
            InputJack = new InputJack();
            OutputJack = new OutputJack(Stream);
        }

        public abstract int Read(byte[] buffer, int offset, int count);
    }
}
