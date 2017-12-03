using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCPA_OTS_Server
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

            //Create console window
            MainWindow ConsoleWindow = new MainWindow();

            //Run Server in new thread
            Task.Run(() => {
                NetConnection AsyncServer = new NetConnection(ConsoleWindow);
            });

            //Show console
            ConsoleWindow.Show();

            //Run the app
            Application.Run();
        }

    }
}
