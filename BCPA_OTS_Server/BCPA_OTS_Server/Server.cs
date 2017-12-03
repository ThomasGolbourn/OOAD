using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BCPA_OTS_Server
{
    class Server
    {
        private IPAddress ServerIP;
        private int ServerPort;
        private IPEndPoint  ServerEndPoint;
        private TcpListener ServerListener;
        public  MainWindow ConsoleWindow;

        public Server()
        {
            InitValues();
            StartServer();
        }

        private void InitValues() {
            //Set address values
            ServerPort = 2200;
            ServerIP = new IPAddress(new byte[] { 127, 0, 0, 1 });
            ServerEndPoint = new IPEndPoint(ServerIP, ServerPort);

            //Create console window
            ConsoleWindow = new MainWindow();
            ConsoleWindow.Show();
        }

        private void StartServer()
        {
            //Create Listener
            ServerListener = new TcpListener(ServerEndPoint);

            //Start Listener
            ServerListener.Start();

            //Log Listener Startup
            ConsoleWindow.Log("Server TCP Listener Online @ " + ServerEndPoint.ToString());

            //Get connections
            GetConnections();
        }

        private async void GetConnections()
        {
            while (Application.OpenForms.Count > 0)
            {
                try
                {
                    TcpClient c = await ServerListener.AcceptTcpClientAsync().ConfigureAwait(false);
                    NewClient newClient = new NewClient(c, ConsoleWindow);
                } catch { }
            }
        }
    }

}
