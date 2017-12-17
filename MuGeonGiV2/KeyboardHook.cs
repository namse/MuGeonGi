using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMUtils.KeyboardHook;

namespace MuGeonGiV2
{
    public static class KeyboardHook
    {
        public static Hook Hook;

        public static void Initialize()
        {
            Hook = new Hook("I'm your father");
        }
    }
}
