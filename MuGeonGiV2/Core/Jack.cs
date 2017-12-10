using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public interface IJack : IInstrument
    {
        void Connect(Cable cable);
    }
}
