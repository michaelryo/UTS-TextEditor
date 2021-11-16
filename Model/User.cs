using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment_2.Model
{
    
    public class User
    {
        //User Attribute
        //All in string because no action needed (no calculation, etc.)
        private string username, password, access, Fname, Lname, DoB;
        public User(string username, string password, string access, string Fname, string Lname, string DoB)
        {
            this.username = username;
            this.password = password;
            this.access = access;
            this.Fname = Fname;
            this.Lname = Lname;
            this.DoB = DoB;
        }
        public string getUsername()
        {
            return this.username;
        }
        public string getPassword()
        {
            return this.password;
        }
        public string getAccess()
        {
            return this.access;
        }
        public string getFname()
        {
            return this.Fname;
        }
        public string getLname()
        {
            return this.Lname;
        }
        public string getDoB()
        {
            return this.DoB;
        }
    }
}
