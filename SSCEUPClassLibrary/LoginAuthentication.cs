using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class LoginAuthentication
    {
        private List<User> listOfUserInfo = new List<User>();

        public void AddUser(string userName, string userPassword)
        {
            User user = new User(userName, userPassword);
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

    }
}