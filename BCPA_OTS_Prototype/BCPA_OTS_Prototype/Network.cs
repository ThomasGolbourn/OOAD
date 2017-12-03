using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using System.Net;

namespace BCPA_OTS_Prototype
{
    public class Network
    {
        private UInt32 clientGUID;
        private TcpClient tcpClient = new TcpClient();
        private NetworkStream netStream;
        private Socket tcpSock;
        private bool connectionOpen = false;

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // State object for receiving data from remote device.  
        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 256;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
            // Received data string.  
            public StringBuilder sb = new StringBuilder();
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket tempSock = (Socket)ar.AsyncState;

                // Complete the connection.  
                tempSock.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    tempSock.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public bool OpenConnection() {
            bool result = false;

            try {

                //Set up ip
                IPAddress ipAdd = Dns.GetHostEntry("127.0.0.1").AddressList[0];
                IPEndPoint ipEnd = new IPEndPoint(ipAdd, 2200);

                //Make sock
                tcpSock  = new Socket(ipAdd.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect to the remote endpoint.  
                tcpClient.BeginConnect(ipAdd, ipEnd.Port,
                    new AsyncCallback(ConnectCallback), tcpSock);
                connectDone.WaitOne();

                netStream = tcpClient.GetStream();
                tcpSock = tcpClient.Client;
            
                //Send bytes
                Byte[] sendBytes = Encoding.UTF8.GetBytes("CONN_OPEN:HELLO");

                //Send message
                netStream.Write(sendBytes, 0, sendBytes.Length);

                //Receive response, this is blocking
                byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

                // Returns the data received from the host to the console.
                string returndata = Encoding.UTF8.GetString(bytes).TrimEnd(new char[] { (char)0 });

                //Log data
                Console.WriteLine("Network:OpenConnection:Data: " + returndata);

                //Check if return data is response
                if (returndata.StartsWith("CONN_ACK:")) {
                    
                    //We have a connection
                    result = true;
                    connectionOpen = true;

                    //Get GUID
                    clientGUID = Convert.ToUInt32(returndata.Substring("CONN_ACK:".Length, returndata.Length - "CONN_ACK:".Length));

                    //Log
                    Console.WriteLine("Network:OpenConnection: Connection Acknowledged, GUID: " + clientGUID.ToString());
                };

            }
            catch (Exception e)
            {
                result = false;
                Console.WriteLine("Network:OpenConnection:Exception: " + e.Message);                
            }

            return result;
        }

   

        public bool AddNewUser(
            string title,           string firstName,       string middleName,  string lastName,    string emailAddress,
            string password,        string country,         string county,      string houseNumber, string addressLine1,
            string addressLine2,    string addressLine3,    string townCity,    string postCode,    DateTime dateOfBirth) {

            //Create result val
            bool result = false;

            //Create byte string to send
            Byte[] sendBytes;

            //Build bytes string
            StringBuilder paramStringBuilder = new StringBuilder();
            paramStringBuilder.Append("ADD_USER:[");
            paramStringBuilder.Append(title);               paramStringBuilder.Append(":");
            paramStringBuilder.Append(firstName);           paramStringBuilder.Append(":");
            paramStringBuilder.Append(middleName);          paramStringBuilder.Append(":");
            paramStringBuilder.Append(lastName);            paramStringBuilder.Append(":");
            paramStringBuilder.Append(emailAddress);        paramStringBuilder.Append(":");
            paramStringBuilder.Append(password);            paramStringBuilder.Append(":");
            paramStringBuilder.Append(country);             paramStringBuilder.Append(":");
            paramStringBuilder.Append(county);              paramStringBuilder.Append(":");
            paramStringBuilder.Append(houseNumber);         paramStringBuilder.Append(":");
            paramStringBuilder.Append(addressLine1);        paramStringBuilder.Append(":");
            paramStringBuilder.Append(addressLine2);        paramStringBuilder.Append(":");
            paramStringBuilder.Append(addressLine3);        paramStringBuilder.Append(":");
            paramStringBuilder.Append(townCity);            paramStringBuilder.Append(":");
            paramStringBuilder.Append(postCode);            paramStringBuilder.Append(":");
            paramStringBuilder.Append(dateOfBirth.ToString("dd/M/yyyy")); //Convert to string with format
            paramStringBuilder.Append("]");

            //Convert to bytes
            sendBytes = Encoding.UTF8.GetBytes(paramStringBuilder.ToString());

            //Log
            Console.WriteLine("Sending: " + paramStringBuilder.ToString());

            //Send bytes
            Encoding.UTF8.GetBytes("ADD_USER:[Title?FirstName?LastName]");

            //Send message
            netStream.Write(sendBytes, 0, sendBytes.Length);

            //Receive response, this is blocking
            byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
            netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);
            
            // Returns the data received from the host to the console.
            string returndata = Encoding.UTF8.GetString(bytes).TrimEnd(new char[] { (char)0 });

            //If success
            if (returndata.StartsWith("ADD_USER_ACK:SUCCESS")) {

                result = true;

            } else {

                result = false;
            }

            //Log data
            Console.WriteLine("Network:AddNewUser:Data: " + returndata);

            return result;
        }


        public void SendLogin(string user, string pass) {

                
            try
            {

                Console.WriteLine("SendLogin:socket connected: " + tcpSock.Connected.ToString());
                Console.WriteLine("SendLogin:can write: " + netStream.CanWrite.ToString());
                Console.WriteLine("SendLogin:client connected: " + tcpClient.Connected.ToString());

                //Send bytes
                Byte[] sendBytes = Encoding.UTF8.GetBytes("LOGIN:[" + user + ":" + pass + "]");

                //Send message
                netStream.Write(sendBytes, 0, sendBytes.Length);

                Console.WriteLine("SendLogin:can read: " + netStream.CanRead.ToString());
                
                //Receive response, this is blocking
                byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

                // Returns the data received from the host to the console.
                string returndata = Encoding.UTF8.GetString(bytes).TrimEnd(new char[] { (char)0 });

                //Log data
                Console.WriteLine("Network:SendLogin:Data: " + returndata);

                //Exit
                return;
            } catch (Exception e){
                
                //Log error
                Console.WriteLine("sendLogin:e:" + e.Message);

                //Open a connection if needed
               // if (!netStream.CanWrite) { tcpSock.Shutdown(SocketShutdown.Both); tcpSock.Disconnect(true); OpenConnection(); };
            }

        }

    }
}
