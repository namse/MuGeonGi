using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSCore;

namespace MuGeonGiV2.Core
{
    public class Switch : Effector
    {
        private bool _isOpen = false;
        private readonly List<ICircuitNode> _empty = new List<ICircuitNode>();
        public override List<ICircuitNode> Nexts => _isOpen ? _empty : OutputJacks.Cast<ICircuitNode>().ToList();
        public override List<ICircuitNode> Previouses => _isOpen ? _empty : InputJacks.Cast<ICircuitNode>().ToList();

        public void SetIsOpen(bool isOpen)
        {
            if (isOpen)
            {
                OutputJacks.ForEach(jack => jack.Cable?.OnDisonnect());
                _isOpen = true;
            }
            else
            {
                _isOpen = false;
                OutputJacks.ForEach(jack => jack.Cable?.OnConnect());
            }
        }

        public void Close()
        {
        }
        
        public override IWaveSource AppendSource(IWaveSource source)
        {
            return source;
        }
    }
}
