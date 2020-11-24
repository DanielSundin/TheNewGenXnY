using System.Collections.Generic;
using System.Linq;

namespace SSCEUPClassLibrary
{
    public class LoginAuthentication
    {
        DBhandler dbHandler = new DBhandler("Server=40.85.84.155;Database=OOPGroup4;User=Student22;Password=zombie-virus@2020;");
       
        public int CheckLoginInfo(string inputName, string inputPass)
        {
            User loginUser = dbHandler.GetUser(inputName).FirstOrDefault();

            if (loginUser == null)
            {
                return 1;
            }
            else if (inputName == loginUser.UserName && inputPass == loginUser.UserPass && loginUser.IsAdmin == true)
            {
                return 3;
            }
            else if (inputName == loginUser.UserName && inputPass == loginUser.UserPass && loginUser.IsAdmin == false)
            {
                return 2;
            }
            else if (inputName != loginUser.UserName || inputPass != loginUser.UserPass)
            {
                return 1;
            }
            return 1;
        }
    }
}