using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ogama.Modules.Rta.RtaReplay
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new Form2());



            //Application.Run(new Form4RtaEventEditor());


            Application.Run(new FormRtaView());

            //Application.Run(new LayoutTestForm());
            

        }
    }
}
