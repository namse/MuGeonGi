using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class EditableStream : Stream
    {
        private Func<byte[], int, int, int> ReadFunc;
        public EditableStream(Func<byte[], int, int, int> readFunc)
        {
            ReadFunc = readFunc;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return ReadFunc(buffer, offset, count);
        }

    }
}
