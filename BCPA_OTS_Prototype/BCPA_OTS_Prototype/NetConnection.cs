using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace BCPA_OTS_Prototype
{
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

    public class NetConnection
    {
        //Used to see if connection is open
        public bool connectionOpen;

        //Used in send/receive
        public Socket currentSock;

         // ManualResetEvent instances signal completion.  
        private ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private String response = String.Empty;

        public NetConnection(Form sourceForm, string dataRequest)
        {
              StartClient(sourceForm, dataRequest);
        }

        private void Req_Login(LoginForm sourceForm, string loginRequest, Socket client){

            //Log
            Console.WriteLine("req_login:Sending: " + loginRequest);

            // Send request to remote host  
            Send(client, loginRequest);
            sendDone.WaitOne(10);

            //If send fails due to no connection, exit, error message will be shown by send operation
            if (!connectionOpen) { client.Shutdown(SocketShutdown.Both); client.Close(); return; };

            // Receive the response from the remote device.  
            Receive(client);
            receiveDone.WaitOne(10);

            //If receive fails due to no connection, exit, error message will be shown by receive operation
            if (!connectionOpen) { client.Shutdown(SocketShutdown.Both); client.Close(); return; };

            //  Wait for response for up to 3sec, set cursor to wait while we do so, disable button and input boxes while we do so
            sourceForm.btn_Login.Enabled = false; sourceForm.tb_PasswordInput.Enabled = false; sourceForm.tb_UsernameInput.Enabled = false;
            sourceForm.btn_Login.Text = "Wait...";
            sourceForm.Cursor = Cursors.WaitCursor;
            DateTime ThreeSecsTime = DateTime.Now.AddSeconds(3);                   int waitTime = 0;
            while (response == "" && DateTime.Now < ThreeSecsTime) { Thread.Sleep(10); waitTime++; }; waitTime = waitTime * 10;

            //Log with wait time
            Console.WriteLine("req_login:"+ waitTime.ToString() + ":Response: " + response);

            //Exit with error on non-response and re-enable buttons and text box and change cursor
            if (response == ("")) {
                sourceForm.errorMessage("The connection timed out, please try again.");
                sourceForm.btn_Login.Enabled = true; sourceForm.tb_PasswordInput.Enabled = true; sourceForm.tb_UsernameInput.Enabled = true;
                sourceForm.btn_Login.Text = "Login";
                sourceForm.Cursor = Cursors.Default;
                return;
            };

            //Exit with error on failed login and re-enable buttons and text box and change cursor
            if (response.StartsWith("LOGIN_FAIL")){
                sourceForm.errorMessage("Login failed, please check your username and password then try again.");
                sourceForm.btn_Login.Enabled = true; sourceForm.tb_PasswordInput.Enabled = true; sourceForm.tb_UsernameInput.Enabled = true;
                sourceForm.btn_Login.Text = "Login";
                sourceForm.Cursor = Cursors.Default;
                return;
            };

            //Exit with error on unknown response and re-enable buttons and text box and change cursor
            if (!response.StartsWith("LOGIN_SUCCESS")) {
                sourceForm.errorMessage("Unknown response received, login failed.");
                sourceForm.btn_Login.Enabled = true; sourceForm.tb_PasswordInput.Enabled = true; sourceForm.tb_UsernameInput.Enabled = true;
                sourceForm.btn_Login.Text = "Login";
                sourceForm.Cursor = Cursors.Default;
                return;
            };

            //  Logged in successfully
            //Change button text
            sourceForm.btn_Login.Text = "Loading";
            //Split response into array
            //Remove [ and ] from data
            response = response.TrimStart(new char[] { '[' });
            response = response.TrimEnd(new char[] { ']' });
            //Split data by : char
            string[] responseArray = response.Split(':');
            //Extract communication GUID from response
            UInt32 communicationGUID = Convert.ToUInt32(responseArray[1]);
            //Extract database id from response
            UInt32 databaseID = Convert.ToUInt32(responseArray[2]);
            //Extract privilege level from response
            string privilegeLevel = responseArray[3];
            //Extract personal info from response
            List<string> personalInfo = new List<string>();
            personalInfo.Add(responseArray[4]);
            personalInfo.Add(responseArray[6]);
            personalInfo.Add(responseArray[7]);
            personalInfo.Add(responseArray[8]);
            personalInfo.Add(responseArray[9]);
            personalInfo.Add(responseArray[10]);
            personalInfo.Add(responseArray[11]);
            personalInfo.Add(responseArray[12]);
            personalInfo.Add(responseArray[13]);
            personalInfo.Add(responseArray[14]);
            personalInfo.Add(responseArray[15]);
            personalInfo.Add(responseArray[16]);
            personalInfo.Add(responseArray[17]);
            personalInfo.Add(responseArray[18]);

            //Decide what to do next based on privilage level
            switch (privilegeLevel.ToUpper())
            {
                case "VENUEMANAGER":
                    {
                        Console.WriteLine("Setting up Venue Manager login...");
                        //Create venue manager object, with data included

                        //Load venue manager interface

                        break;
                    }
                case "AGENT":
                    {
                        Console.WriteLine("Setting up Agent login...");
                        //Create agent object, with data included

                        //Load customer interface for agent

                        break;
                    }
                case "CUSTOMER":
                    {
                        Console.WriteLine("Setting up Customer login...");
                        //Create customer object, with data included

                        //Load customer interface


                        break;
                    }
                default:
                    {
                        //Unknown user type, show error
                        sourceForm.errorMessage("Unknown response received, login failed.");
                        break;
                    }
                    
            }

        }


        private void Req_Register (RegistrationForm sourceForm, string registerRequest, Socket client){
           
            //Log
            Console.WriteLine("req_register:Sending: " + registerRequest);


        }


        private void HandleDataRequest(Form sourceForm, string dataRequest, Socket client)
        {
            SByte typeNum = 0;

            //Get Form Type
            if (sourceForm.GetType().Name == "Form")                { typeNum = 0; };
            if (sourceForm.GetType().Name == "LoginForm")            { typeNum = 1; };
            if (sourceForm.GetType().Name == "RegistrationForm")    { typeNum = 2; };

            //Use form type
            switch (typeNum)
            {
                    //Login Form
                    case 1:{

                        //Parse form as it's own data type
                        LoginForm srcFrm = (LoginForm) sourceForm;

                        //Login
                        if (dataRequest.Contains("LOGIN")) { Req_Login(srcFrm, dataRequest, client);  };

                        //Exit Switch
                        break;
                    }
                     //Registration Form
                    case 2:{

                        //Parse form as it's own data type
                        RegistrationForm srcFrm = (RegistrationForm) sourceForm;

                        //Register account
                        if (dataRequest.Contains("ADD_USER")) { Req_Register(srcFrm, dataRequest, client); };

                        //Exit Switch
                        break;
                    }
                    default:{

                        //Wait for response, set cursor to wait
                        sourceForm.Cursor = Cursors.WaitCursor;
                        Thread.Sleep(1000);
                        sourceForm.Cursor = Cursors.Default;

                        //Exit switch
                        break;
                    }    
            }

        }


        public void StartClient(Form sourceForm, string dataRequest)
        {
            // Connect to a remote device.  
            try
            {
                // Establish the remote endpoint for the socket.  
                // The name of the   
                // remote device is "host.contoso.com".  
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Properties.Settings.Default.serverAddress);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, (int) Properties.Settings.Default.serverPort);

                // Create a TCP/IP socket.  
                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                //Update current socket
                currentSock = client;

                //Set connection open value
                connectionOpen = true;

                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);

                //Wait while we get connection
                connectDone.WaitOne(1);

                //Parse to data handler for send/receives
                HandleDataRequest(sourceForm, dataRequest, client);

                // Release the socket. 
                client.Shutdown(SocketShutdown.Both);
                client.Close();

            }
            catch (Exception e)
            {
                //Show warning box
                MessageBox.Show(
                    e.Message.ToString(),
                    "Connection Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );

                Console.WriteLine("AsyncClient:StartClient:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                //Show warning box
                MessageBox.Show(
                    "Cannot connect to server.",
                    "Connection Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );

                Console.WriteLine("AsyncClient:ConnectCallback:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine("AsyncClient:Receive:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("AsyncClient:ReceiveCallback:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }
        }

        public void Send(Socket client, String data)
        {
            try
            {
                // Convert the string data to byte data using ASCII encoding.  
                byte[] byteData = Encoding.ASCII.GetBytes(data + Properties.Settings.Default.dataTerminator);

                //Log sent data
                Console.WriteLine("Sending: " + data + Properties.Settings.Default.dataTerminator);

                // Begin sending the data to the remote device.  
                client.BeginSend(byteData, 0, byteData.Length, 0,
                    new AsyncCallback(SendCallback), client);
            }
            catch (Exception e)
            {
                Console.WriteLine("AsyncClient:Send:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }

        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine("AsyncClient:SendCallback:" + e.ToString());

                //Set connection open value
                connectionOpen = false;
            }
        }
    }
}
