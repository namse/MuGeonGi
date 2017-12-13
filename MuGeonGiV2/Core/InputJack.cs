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
        public FakeStream FakeStream = new FakeStream();
        public override void Connect(Cable cable)
        {
            FakeStream.SetStream(cable.FakeStream);
        }

        public void Destroy()
        {
            throw new NotImplementedException();
        }
    }
}
