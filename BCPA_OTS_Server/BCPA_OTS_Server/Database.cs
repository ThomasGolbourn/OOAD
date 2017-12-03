using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace BCPA_OTS_Server
{
    class Database
    {
        //Get settings for connection from application built in settings
        private string serverAddress = Properties.Settings.Default.db_Address;
        private uint serverPort = Properties.Settings.Default.db_Port;
        private string serverUser = Properties.Settings.Default.db_Username;
        private string serverPass = Properties.Settings.Default.db_Password;
        private string serverDb = Properties.Settings.Default.db_Database;

        //Database variables
        private MySqlConnection DbConn;

        // Opens DB connection
        public void OpenConn()
        {
            try
            {
                //Set-Up Database Connection
                MySqlConnectionStringBuilder MySqlConnectionString = new MySqlConnectionStringBuilder();
                MySqlConnectionString.Server = serverAddress; MySqlConnectionString.Port = serverPort;
                MySqlConnectionString.UserID = serverUser; MySqlConnectionString.Password = serverPass;
                MySqlConnectionString.Database = serverDb;

                //Connect to Database
                DbConn = new MySqlConnection(MySqlConnectionString.GetConnectionString(true));
                DbConn.Open();

                //Return void
                return;

            }
            catch (Exception e)
            {
                //Show warning box
                MessageBox.Show(
                    "Could not establish database connection. \nException Code(s): " + e.Message,
                    "Connection Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );

                //return void
                return;
            }
        }

        //Close connection
        public void CloseConn()
        {
            DbConn.Close();
        }

        //Checks credentials for login
        public string LogIn(string user, string pass)
        {
            //Returned success/fail of login
            bool result = false;
            //Returned string
            string resultString = "FAILURE";

            try {
            
                //Open connection
                OpenConn();

                //Create query
                string loginQuery = ("SELECT * From " + serverDb + ".UserData where EmailAddress ='" + user + "' and PasswordHash ='" + pass + "'");

                //Run query
                MySqlDataReader DbDataReader;
                MySqlCommand AddUserCommand = new MySqlCommand(loginQuery, DbConn);
                DbDataReader = AddUserCommand.ExecuteReader();

                //Read query result
                while (DbDataReader.Read()) { };

                //Get result field count
                int fieldCount = DbDataReader.FieldCount;

                //Build success string if we have success
                if (fieldCount > 0) {

                    StringBuilder successStringBuilder = new StringBuilder();
                    successStringBuilder.Append("[SUCCESS");

                    //For each received part of query
                    for (int i = 0; i < (fieldCount); i++){
                        successStringBuilder.Append(":");
                        successStringBuilder.Append(DbDataReader[i].ToString());
                    };

                    //Close off
                    successStringBuilder.Append("]");

                    //Set to result
                    resultString = successStringBuilder.ToString();
                }

                //Close reader
                DbDataReader.Close();

                //Close connection after query finishes
                CloseConn();

            } catch(Exception e) {

                //Update result
                resultString = "FAILURE:" + e.Message;

                //Log error
                Console.WriteLine("Exception: " + e.Message);

                //Close connection after query finishes
                CloseConn();

            };

            return resultString;
        }


        //Registers new user account in db
        public void AddNewUser(string title, string firstName, string middleName, string lastName, string emailAddress, string password, string country, string county, string houseNumber, string addressLine1, string addressLine2, string addressLine3, string townCity, string postCode, string dateOfBirth)
        {
            try {

                //Open connection
                OpenConn();

                //Add User to DB
                string AddUserQuery = ("Insert into " + serverDb + ".UserData(" +
                    "EmailAddress," +
                    "PasswordHash," +
                    "Title," +
                    "FirstName," +
                    "MiddleName," +
                    "LastName," +
                    "Country," +
                    "County," +
                    "HouseNumber," +
                    "AddressLine1," +
                    "AddressLine2," +
                    "AddressLine3," +
                    "TownCity," +
                    "PostCode," +
                    "DateOfBirth" +
                    ") values (" +
                    "'" + emailAddress + "'," +
                    "'" + password + "'," +
                    "'" + title + "'," +
                    "'" + firstName + "'," +
                    "'" + middleName + "'," +
                    "'" + lastName + "'," +
                    "'" + country + "'," +
                    "'" + county + "'," +
                    "'" + houseNumber + "'," +
                    "'" + addressLine1 + "'," +
                    "'" + addressLine2 + "'," +
                    "'" + addressLine3 + "'," +
                    "'" + postCode + "'," +
                    "'" + townCity + "'," +
                    "'" + dateOfBirth +
                    "');"
                );

                //Run query
                MySqlDataReader DbDataReader;
                MySqlCommand AddUserCommand = new MySqlCommand(AddUserQuery, DbConn);
                DbDataReader = AddUserCommand.ExecuteReader();

                //While query running
                while (DbDataReader.Read()) { };

                //Close connection after query finishes
                CloseConn();

                //Exit
                return;

            }
            catch (Exception e)
            {
                //Show warning box
                MessageBox.Show(
                    "Could not create new user. \nException Code(s): " + e.Message,
                    "User Creation Error!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1
                );

                //Attempt to close connection
                CloseConn();

                //Exit
                return;
            }
        }
    }
}
