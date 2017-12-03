using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace BCPA_OTS_Server
{
    class NewClient
    {
        private Database db;
        private MainWindow mainWin;
        private TcpClient tcpClient;
        private Socket tcpSock;
        private NetworkStream netStream;
        private bool connectionOpen;
        private uint clientGUID;
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        private static char[] blankData = new char[]{ (char)0 };
        private string LogText;

        /* Initialiser */
        public NewClient(TcpClient c, MainWindow f){

            //Get main form
            mainWin = f;

            //Create GUID
            Byte[] randByteArr = new byte[4];
            rngCsp.GetBytes(randByteArr);
            uint randInt = BitConverter.ToUInt32(randByteArr, 0);
            clientGUID = randInt;

            //Set-up Client Values
            tcpClient = c;
            netStream = c.GetStream();
            tcpSock = tcpClient.Client;

            //Init database handler
            db = new Database();

            //Log Connection
            LogText = ("New Connection from:\t" + tcpSock.RemoteEndPoint.ToString() + "\t GUID: " + clientGUID.ToString());
            Console.WriteLine(LogText); mainWin.Log(LogText);

            //Check Connection
            CheckConnection();

            //Start Listening to Client
            StreamListener();
        }

        /*  Connection Checker */
        private void CheckConnection() {
            try {
                if (tcpSock.Poll(0, SelectMode.SelectRead)) {
                    if (tcpSock.Receive(new byte[1], SocketFlags.Peek) == 0) { connectionOpen = false; } else { connectionOpen = true; };
                } else { connectionOpen = false; };
            } catch { connectionOpen = false; };
        }

        /*  Network Stream Listener */
        private void StreamListener() {

            //While connection is open, read data
            while (connectionOpen) {
                
                //Check connection before trying things
                CheckConnection();

                try {
                    //If stream is readable
                    if (netStream.CanRead)
                    {

                        //Read stream
                        // Reads NetworkStream into a byte buffer.
                        byte[] bytes = new byte[tcpClient.ReceiveBufferSize];

                        // Read can return anything from 0 to numBytesToRead. 
                        // This method blocks until at least one byte is read.
                        netStream.Read(bytes, 0, (int)tcpClient.ReceiveBufferSize);

                        // Returns the data received from the host to the console.
                        string networkString = Encoding.UTF8.GetString(bytes);

                        //Go back to start if data is blank
                        if (networkString == "") { continue; };

                        //Trim data
                        networkString = networkString.TrimEnd(blankData);

                        //Log data
                        Console.WriteLine("Data: " + networkString);

                        //Process Data
                        ProcessClientData(networkString);
                    }
                } catch { }             
            }

            //Conneciton has been lost
            LogText = ("Lost Connection with:\t" + tcpSock.RemoteEndPoint.ToString() + "\t GUID: " + clientGUID.ToString());
            Console.WriteLine(LogText); mainWin.Log(LogText);

            //Object is given to garbage collector automatically
        }

        /* Received Data Processor */
        private void ProcessClientData(string clientRequest)
        {
            //Requests come in format: COMMAND:DATA
            Console.WriteLine("clientRequest: " + clientRequest.ToString());

            //Command and data split using :
            string[] requestArray = clientRequest.Split(new char[] { ':' }, 2);

            //If format not recognised, exit here
            if (requestArray.Length < 2) { return; }

            //Split into command and request strings
            string requestCommand = requestArray[0];
            string requestData = requestArray[1];

            //Log request and data
            LogText = clientGUID.ToString() + ": Command: '" + requestCommand + "' Data: '" + requestData + "'"; 
            Console.WriteLine(LogText); mainWin.Log(LogText);

            //Act upon the strings
            if (requestCommand == "CONN_OPEN") { tcpSock.Send(Encoding.UTF8.GetBytes("CONN_ACK:"+clientGUID.ToString())); }; //Send a ping back
            if (requestCommand == "ADD_USER") { ClientRequest_AddUser(requestData); };          //Add customer to db
            if (requestCommand == "LOGIN") { ClientRequest_Login(requestData); }; //Send a ping back
        }

        /* Log Into Account */
        private void ClientRequest_Login(string clientData) {
            //clientData format:  [Username:Password]

            //Remove [ and ] from data
            clientData = clientData.TrimStart(new char[] { '[' });
            clientData = clientData.TrimEnd(new char[] { ']' });

            //Split data by : char
            string[] loginArray = clientData.Split(':');
            string user = loginArray[0]; string pass = loginArray[1];

            //Query db to see if an account matches
            string loginResultString = db.LogIn(user, pass);
            bool loginResultSuccess = loginResultString.StartsWith("[SUCCESS");

            Console.WriteLine("LOGIN_ACK:[" + clientGUID.ToString() + ":" + loginResultString);

            //Send response to client
            tcpSock.Send(Encoding.UTF8.GetBytes("LOGIN_ACK:[" + clientGUID.ToString() + ":" + loginResultString));

            //If login success / fail
            if (loginResultSuccess)
            {
                //Login success
               

            } else {

                //Login fail

            }
        }

        /*  Register new Account */
        private void ClientRequest_AddUser(string clientData)
        {
            //clientData format:  [Title,FirstName,MiddleName,LastName ... ]

            //Remove [ and ] from data
            clientData = clientData.TrimStart(new char[] { '[' });
            clientData = clientData.TrimEnd(new char[] { ']' });

            //Split data by : char
            string[] addUserArray = clientData.Split(':');

            //Log
            foreach (string arrayPart in addUserArray){
                Console.WriteLine("\t" + arrayPart);
            }

            //Open connection to db and add new user
            db.AddNewUser(
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
            tcpSock.Send(Encoding.UTF8.GetBytes("ADD_USER_ACK:SUCCESS:" + clientGUID.ToString()));
        }
    }
}
