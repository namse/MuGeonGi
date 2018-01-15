using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class InputJack : Jack
    {
        public override List<ICircuitNode> Nexts =>
            Instrument == null
                ? new List<ICircuitNode>()
                : new List<ICircuitNode>() { Instrument };

        public override List<ICircuitNode> Previouses =>
            Cable == null
                ? new List<ICircuitNode>()
                : new List<ICircuitNode>() { Cable };

        public InputJack(Instrument instrument) : base(instrument)
        {
        }
    }
}
