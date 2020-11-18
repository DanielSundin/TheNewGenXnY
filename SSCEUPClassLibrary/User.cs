namespace SSCEUPClassLibrary
{
    public class User
    {
        public int Id { get; set; }
        // public string FirstName { get; set; }
        // public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        // Denna kan tas bort när vi har Databasen uppe. Vi kommer bara ha 1 admin sen. 
        public User(string userName, string password, bool isAdmin)
        {
            this.UserName = userName;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }

        // public User(string firstName, string userName, string userPassword)
        // {

        //     this.FirstName = firstName;
            
          
        // }
    }
}