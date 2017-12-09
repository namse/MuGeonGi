using CSCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuGeonGiV2.Core
{
    public class InputJack : IJack
    {
        public FakeStream FakeStream = new FakeStream();
        public void Connect(Cable cable)
        {
            FakeStream.SetStream(cable.FakeStream);
        }
    }
}
