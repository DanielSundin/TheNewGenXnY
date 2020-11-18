using System.Collections.Generic;
using System.Linq;

namespace SSCEUPClassLibrary
{
    public class LoginAuthentication
    {
        DBhandler dbHandler = new DBhandler("Server=40.85.84.155;Database=OOPGroup4;User=Student22;Password=zombie-virus@2020;");
        private List<User> listOfUserInfo = new List<User>();

        public void AddNewUser(string userName, string userPassword)  //behövs?
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

        public int CheckLoginInfo(string inputName, string inputPass)
        {
            User loginUser = (User)dbHandler.GetUser(inputName,inputPass);
            if (inputName == loginUser.UserName && inputPass == loginUser.Password)
            {
                return 2;
            }
            else if (inputName == loginUser.UserName && inputPass == loginUser.Password && loginUser.IsAdmin == true)
            {
                return 3;
            }
            // else if(inputName != loginUser.UserName)             // ifall att user inte hittas i databasen
            // {                                                    // kan man få möjligheten att skapa en ny användare 
            //    
            // return 4;         =>  WouldYouLikeToCreateNewUser();
            // }
            return 1;
        }



        // public bool IsAdmin(string inputName, string inputPass)
        // {
        //     User loginUser = (User)dbHandler.GetUser(inputName, inputPass);
        //     if (loginUser.IsAdmin == true)
        //     {
        //         return true;
        //     }
        //     return false;
        // }
    }
}