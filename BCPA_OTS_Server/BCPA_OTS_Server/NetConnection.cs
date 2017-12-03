using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace BCPA_OTS_Server
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }

    public class NetConnection
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        //Random number gen
        private static RNGCryptoServiceProvider rngCsp;

        //Database connection
        Database db;

        //Main console window
        MainWindow mainWin;

        public NetConnection(MainWindow mWin)
        {
            //Set Main Window up
            mainWin = mWin;

            //Set database up
            db = new Database();

            //Set up crypto provider
            rngCsp = new RNGCryptoServiceProvider();

            //Run server listener
            StartListening();
        }

        /* Log Into Account */
        private void Req_Login(Socket handler, string clientData)
        {
            //clientData format:  [Username:Password]

            //Log
            mainWin.Log(handler.RemoteEndPoint.ToString() + ":Login Request");

            //Remove [ and ] from data
            clientData = clientData.TrimStart(new char[] { '[' });
            clientData = clientData.TrimEnd(new char[] { ']' });

            //Split data by : char
            string[] loginArray = clientData.Split(':');
            string user = loginArray[0]; string pass = loginArray[1];

            //Query db to see if an account matches
            string loginResultString = db.LogIn(user, pass);
            bool loginResultSuccess = loginResultString.StartsWith("[SUCCESS");

            //If login success / fail
            if (loginResultSuccess)
            {
                //  Login success
                //Create GUID
                Byte[] randByteArr = new byte[4];
                rngCsp.GetBytes(randByteArr);
                uint randInt = BitConverter.ToUInt32(randByteArr, 0);
                uint clientGUID = randInt;

                //Show login in console
                mainWin.Log(clientGUID + ":Login Success: " + loginResultString);
                Console.WriteLine(clientGUID + ":Login Success: " + loginResultString);
               
                //Create login success packet
                // Login response, GUID, Database data
                StringBuilder loginSuccessSb = new StringBuilder();
                loginSuccessSb.Append("LOGIN_SUCCESS:");
                loginSuccessSb.Append(clientGUID + ":");
                //Get database data from login result string
                // Format: [SUCCESS:UserID:UserType:EmailAddress:Password:Title:FirstName:MiddleName:LastName:
                //          Country:County:HouseNum:Address1:Address2:Address3:TownCity:PostCode:DateOfBirth]
                //Remove the SUCCESS text and its colon
                loginResultString = loginResultString.Remove(0,9);
                loginResultString = loginResultString.TrimEnd(new char[] { ']' });
                loginSuccessSb.Append(loginResultString);

                //Send login success packet
                Send(handler, loginSuccessSb.ToString());

            }
            else {

                //  Login fail

                //Send login fail packet
                Send(handler, "LOGIN_FAIL");

                //Show login in console
                mainWin.Log(handler.RemoteEndPoint.ToString() + ":Login Fail");
            }
        }

        /*  Register new Account */
        private void Req_AddUser(Socket handler, string clientData)
        {
            //clientData format:  [Title,FirstName,MiddleName,LastName ... ]

            //Remove [ and ] from data
            clientData = clientData.TrimStart(new char[] { '[' });
            clientData = clientData.TrimEnd(new char[] { ']' });

            //Split data by : char
            string[] addUserArray = clientData.Split(':');

            //Log
            foreach (string arrayPart in addUserArray)
            {
                Console.WriteLine("\t" + arrayPart);
            }

            //Open connection to db and add new user
            /*db.AddNewUser(
                addUserArray[0],
                addUserArray[1],
                addUserArray[2],
                addUserArray[3],
                addUserArray[4],
                addUserArray[5],
                addUserArray[6],
                addUserArray[7],
                addUserArray[8],
                addUserArray[9],
                addUserArray[10],
                addUserArray[11],
                addUserArray[12],
                addUserArray[13],
                addUserArray[14]
            );

            //Send acknowledgement
            tcpSock.Send(Encoding.UTF8.GetBytes("ADD_USER_ACK:SUCCESS:" + clientGUID.ToString())); */
        }

        /* Received Data Processor */
        private void HandleClientData(Socket handler, string clientRequest)
        {
            //Remove end of data tag
            clientRequest = clientRequest.Substring(0, clientRequest.Length - "<EOD>".Length);

            //Requests come in format: COMMAND:DATA<EOD>
            Console.WriteLine("clientRequest: " + clientRequest.ToString());

            //Command and data split using :
            string[] requestArray = clientRequest.Split(new char[] { ':' }, 2);

            //If format not recognised, exit here
            if (requestArray.Length < 2) { return; }

            //Split into command and request strings
            string requestCommand = requestArray[0];
            string requestData = requestArray[1];

            //Log request and data
            string LogText = "Command: '" + requestCommand + "' Data: '" + requestData + "'";

            //Act upon the strings
            if (requestCommand == "ADD_USER")   { Req_AddUser(handler, requestData); };
            if (requestCommand == "LOGIN")      { Req_Login(handler, requestData); };  
        }

        public void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);
            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-data tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOD>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    //Process data
                    HandleClientData(handler, content);
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        private void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
