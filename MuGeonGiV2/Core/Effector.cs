using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class Effector : Stream, IInstrument
    {
        public InputJack InputJack = new InputJack();
        public OutputJack OutputJack;

        public Effector()
        {
            OutputJack = new OutputJack(this);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InputJack.FakeStream.Read(buffer, offset, count);
        }

        public void TurnOn()
        {
            // Hmm... We should mute every sound before turning on.
        }
    }
}
