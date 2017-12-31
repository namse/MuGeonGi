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
        public override ICircuitNode Next => _isOpen ? null : OutputJack;
        public override ICircuitNode Previous => _isOpen ? null : InputJack;

        public void SetIsOpen(bool isOpen)
        {
            if (isOpen)
            {
                OutputJack.Cable?.OnDisonnect();
                _isOpen = true;
            }
            else
            {
                _isOpen = false;
                OutputJack.Cable?.OnConnect();
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
