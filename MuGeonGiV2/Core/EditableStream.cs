using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class EditableStream : Stream
    {
        private readonly Func<byte[], int, int, int> _readFunc;
        public EditableStream(Func<byte[], int, int, int> readFunc)
        {
            _readFunc = readFunc;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _readFunc(buffer, offset, count);
        }
    }
}
