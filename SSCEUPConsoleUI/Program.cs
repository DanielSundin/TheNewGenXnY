using System;
//using SSCEUPClassLibrary;
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
            loginauth.AddUser("ADMIN", "qwerty");
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\nLOGIN PROMPT\n");
            Console.ResetColor();
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
                    Console.Clear();
                    System.Console.WriteLine("Welcome to the Bureau");
                    Thread.Sleep(2000);
                    Console.Clear();
                    RunAdminMode();
                }
            }
    }

        private static void RunAdminMode()
        {
            while (true)
            {
                // Admin Menu
                System.Console.WriteLine("\tOptions\n[A]dd Survey\n[L]ist\n[S]earch\n[Q]uit");
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