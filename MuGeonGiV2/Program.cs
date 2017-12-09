using MuGeonGiV2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuGeonGiV2
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var mic = new Mic();
            var effector = new Effector();
            var speaker = new Speaker();

            var cable = new Cable();
            var cable2 = new Cable();

            mic.OutputJack.Connect(cable);
            effector.InputJack.Connect(cable);

            effector.OutputJack.Connect(cable2);
            speaker.InputJack.Connect(cable);

            mic.TurnOn();
            effector.TurnOn();
            speaker.TurnOn();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
