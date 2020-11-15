using System;
using SSCEUPClassLibrary;
using System.Threading;

namespace SSCEUP
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLogin();
        }

        private static void RunLogin()
        {
            LoginAuthentication loginauth = new LoginAuthentication();
            loginauth.AddNewUser("ADMIN", "qwerty", true); // Hårdkodade användare
            loginauth.AddNewUser("TEST", "test"); // Hårdkodade användare
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\nLOGIN PROMPT\n");
            Console.ResetColor();

            //Kanske försöka lyfta ut Loginförsök till egen metod. 
            for (int loginAttempts = 1; loginAttempts <= 3; loginAttempts++)
            {
                System.Console.WriteLine("Enter Username");
                string inputName = Console.ReadLine().ToUpper();
                System.Console.WriteLine("Enter Password");
                string inputPass = Console.ReadLine();

                if (loginauth.CheckLoginInfo(inputName, inputPass) == false)
                {
                    System.Console.WriteLine("Username or Password was incorrect");
                    if (loginAttempts >= 3)
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("To many attempts, try again later.");
                        Environment.Exit(0);
                    }

                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == true)
                {
                    if (loginauth.IsAdmin(inputName,inputPass)==true)
                    {
                        RunAdminMode();
                    }
                    else
                    {
                        RunUserMode();
                    }
                }
            }
        }

     

        private static void RunUserMode()
        {
            while (true)
            {
                // User Menu
                System.Console.WriteLine("\tOptions\n[D]o Survey\n[Q]uit");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "D":
                        {
                            Console.Clear();

                            break;
                        }
                    case "Q":
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

        private static void RunAdminMode()
        {
            while (true)
            {
                // Admin Menu
                System.Console.WriteLine("\tOptions\n[A]dd Survey\n[R]emove Survey\n[Q]uit");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "A":
                        {
                            Console.Clear();

                            break;
                        }
                    case "Q":
                        {
                            Environment.Exit(0);
                            break;
                        }
                }
            }
        }

    }
}