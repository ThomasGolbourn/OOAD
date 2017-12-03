using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SeatingLayout newSeatLayout = new SeatingLayout(Properties.Resources.sampleLayout1);

            //Application.Run(new CreateSeatUI());

            Application.Run(new SeatingLayoutUI(newSeatLayout, true));

            //            try { Application.Run(new LoginForm()); } catch { Application.Exit(); };
        }
    }
}
