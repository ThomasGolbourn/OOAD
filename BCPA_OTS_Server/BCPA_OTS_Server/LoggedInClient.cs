using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Windows.Forms;


namespace BCPA_OTS_Server
{
    class LoggedInClient
    {
        //Key Values
        private UInt32  clientGUID; //Network Identifier
        private Int32   UserID;     //Database Identifier
        private string  UserType;   //Privilege Level
        
        //User information
        private String EmailAddress, PasswordHash, Title, FirstName, MiddleName, LastName, Country, County;
        private String HouseNumber, AddressLine1, AddressLine2, AddressLine3, TownCity, PostCode, DateOfBirth;

        //Network Values
        //private 
    }
}
