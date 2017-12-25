using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;

namespace MuGeonGiV2.Core
{
    public abstract class Effector : Instrument
    {
        public override bool IsEndPoint => false;

        protected Effector()
        {
            InputJack = new InputJack(this);
            OutputJack = new OutputJack(this);
        }

        public abstract IWaveSource AppendSource(IWaveSource source);
    }
}
