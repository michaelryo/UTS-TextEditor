using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Assignment_2.Model
{
    class Users
    {
        private List<User> listUsers = new List<User>();
        private User loginUser;
        //Path to the login.txt
        static readonly string loginFile = @"..\..\..\login.txt";
        public Users()
        {
            // Read entire text file content in one string  
            string[] lines = File.ReadAllLines(loginFile);
            foreach (string line in lines)
            {
                //Split it by the ',' symbol following tutor format
                string[] tempUserDetails = line.Split(',');
                listUsers.Add(new User(
                    tempUserDetails[0].Trim(),
                    tempUserDetails[1].Trim(),
                    tempUserDetails[2].Trim(),
                    tempUserDetails[3].Trim(),
                    tempUserDetails[4].Trim(),
                    tempUserDetails[5].Trim()));
            }
        }
        //Get CurrentUserLogin User
        public User getLoginUser()
        {
            return loginUser;
        }
        //Function for Register
        public void Register(User user)
        {
            //get all line and make it to user object
            string[] dataLine = File.ReadAllLines(loginFile);
            string[] newUser = new string[] { user.getUsername() + "," + user.getPassword() + "," + user.getAccess() + "," + user.getFname() + "," + user.getLname() + "," + user.getDoB() };
            //Add new User to Array, then Replace Text File with Array
            dataLine = dataLine.Concat(newUser).ToArray();
            File.WriteAllLines(loginFile, dataLine);
        }
        public bool usernameExist(string username)
        {
            //Loop through all users
            foreach (User user in listUsers)
            {
                //Check username
                if(user.getUsername().Trim().Equals(username.Trim()))
                    return true;
            }
            return false;
        }

        //checking if user exists.
        public bool UserExist(String username, String password)
        {
            //Loop through all users
            foreach (User user in listUsers)
            {
                //Check username and password
                if (user.getUsername().Trim().Equals(username.Trim()) &&
                    user.getPassword().Trim().Equals(password.Trim()))
                {
                    this.loginUser = user;
                    return true;
                }
            }
            return false;
        }
    }
}
