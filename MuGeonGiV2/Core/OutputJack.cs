using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore.SoundIn;
using CSCore.Streams;
using CSCore.Streams.SampleConverter;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class OutputJack : Jack
    {
        public override List<ICircuitNode> Nexts =>
            Cable == null
            ? new List<ICircuitNode>()
            : new List<ICircuitNode>() { Cable };
        
        public override List<ICircuitNode> Previouses =>
            Instrument == null
                ? new List<ICircuitNode>()
                : new List<ICircuitNode>() { Instrument };

        public OutputJack(Instrument instrument) : base(instrument)
        {
        }
    }
}
