using System.Collections.Generic;
using System.Linq;

namespace SSCEUPClassLibrary
{
    public class LoginAuthentication
    {
        private List<User> listOfUserInfo = new List<User>();

        public void AddNewUser(string userName, string userPassword)
        {
            User user = new User(userName, userPassword);
            listOfUserInfo.Add(user);
        }

        // Ta bort den här metoden när vi kopplar in Databasen. Då finns Admin redan där. 
        public void AddNewUser(string userName, string userPassword, bool IsAdmin)
        {
            User user = new User(userName, userPassword, IsAdmin);
            listOfUserInfo.Add(user);
        }

        public bool CheckLoginInfo(string inputName, string inputPass)
        {
            foreach (var user in listOfUserInfo)
            {
                if (inputName == user.UserName && inputPass == user.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsAdmin(string inputName,string inputPass)
        {
            foreach (var user in listOfUserInfo)
            {
                if (inputName == user.UserName && inputPass == user.Password && user.IsAdmin == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}