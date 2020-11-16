using System;
using SSCEUPClassLibrary;
using System.Threading;
using System.Collections.Generic;

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
                        System.Console.WriteLine("Too many attempts, try again later.");
                        Environment.Exit(0);
                    }

                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == true)
                {
                    if (loginauth.IsAdmin(inputName, inputPass) == true)
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
                            DoSurvey();
                            break;
                        }
                    case "Q":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid Choice!");
                            return;
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
                            DisplaySurveyMenu();

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
        private static void DoSurvey()
        {
            Survey samplesurvey = new Survey();

            while (true)
            {
                foreach (var q in samplesurvey.MakeSampleList())
                {
                    Console.WriteLine(q.ToString());

                    if (q.GetType() == typeof(bool))
                    {
                        Console.WriteLine("Y/N");
                        string input = Console.ReadLine().ToUpper().Trim();
                        switch (input)
                        {
                            case "Y":
                                {
                                    
                                    break;
                                }
                            case "N":
                                {
                                    break;
                                }
                            default:
                                {
                                    Console.WriteLine("Choose Y or N.");
                                    return;
                                }
                        }
                    }
                    else
                    {
                        Console.WriteLine("(1-5)");
                        int input = Convert.ToInt32(Console.ReadLine());        //lägg till try/catch
                        if (input > 0 && input < 6)
                        {
                            System.Console.WriteLine(" här är skalan 1-5!!!!");
                            // q.answer = input;
                        }
                        else
                        {
                            Console.WriteLine("Answer out of range!");
                            return;
                        }
                    }
                }
            }
        }



        private static void DisplaySurveyMenu()
        {
            SurveyManager surveyManager = new SurveyManager();
            while (true)
            {
                //   Console.WriteLine("What kind of Answer do you want?");
                //   Console.WriteLine("[Y]es or No Question\n[S]caleQuestion");
                //    string questionChoice = Console.ReadLine().ToUpper().Trim();     


                //   switch (questionChoice)
                //   {
                // case "S":
                List<Question> listofquestion = new List<Question>();

                Console.WriteLine("What do you want to name the survey?");
                string surveyName = Console.ReadLine();

                bool isDone = false;
                while (isDone == false)
                {
                    //Question test = new ScaleQuestion("hur många bultar har ölandsbron");
                    Console.WriteLine("Question?");
                    string input = Console.ReadLine();
                    Console.WriteLine("Is this a Yes/No Question? No will make the question a scaled question.\n(Y/N)\n");
                    string ynorscalechoice = Console.ReadLine().ToUpper().Trim();
                    switch (ynorscalechoice)
                    {
                        case "Y":
                            listofquestion.Add(new YesNoQuestion(input));
                            break;
                        case "N":
                            listofquestion.Add(new ScaleQuestion(input));
                            break;
                        default:
                            Console.WriteLine("Sorry, (Y)es or (N)o please");
                            return;
                    }

                    Console.WriteLine("Add more Questions? Y/N");
                    string continueInput = Console.ReadLine().ToUpper().Trim();
                    if (continueInput == "N")
                    {
                        isDone = true;
                        foreach (Question q in listofquestion)
                        {
                            Console.WriteLine(q.ToString());
                        }
                    }
                    else if (continueInput == "Y")
                    {
                        isDone = false;
                    }
                }
                surveyManager.CreateNewSurvey(listofquestion, surveyName);

                // break;

                // case "Y":

                //     break;

                // default:
                // break;
            }
        }
    }
}
