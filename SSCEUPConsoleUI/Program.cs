using System;
using SSCEUPClassLibrary;
using System.Threading;
using System.Collections.Generic;

namespace SSCEUP
{
    class Program
    {

        public string currentuser = null;
        static void Main(string[] args)
        {

            RunLogin();
        }

        private static void RunLogin()
        {
            SurveyManager surveyManager = new SurveyManager();

            
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
                        RunAdminMode(surveyManager);
                    }
                    else
                    {
                        RunUserMode(surveyManager);

                    }
                }
            }
        }

        private static void RunUserMode(SurveyManager surveyManager)
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
                            DoSurvey(surveyManager);
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

        private static void RunAdminMode(SurveyManager surveyManager)
        {
            while (true)
            {
                // Admin Menu
                System.Console.WriteLine("\tOptions\n[A]dd Survey\n[D]o Survey\n[R]emove Survey\n[Q]uit");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "A":
                            Console.Clear();
                            DisplaySurveyMenu(surveyManager);

                            break;
                    case "D": 
                            DoSurvey(surveyManager); 
                            break;

                    case "Q":
                            Environment.Exit(0);
                            break;
                }
            }
        }
        private static void DoSurvey(SurveyManager surveyManager)
        {
            // Survey samplesurvey = new Survey();

            if(surveyManager.listOfSurveys.Count == 0)
            {
                return;
            }

            foreach (Survey survey in surveyManager.listOfSurveys)
            {
                System.Console.WriteLine(survey.Name);
                System.Console.WriteLine();
            }


            while (true)
            {
                // foreach (Question q in samplesurvey.MakeSampleList())
                foreach (Question q in surveyManager.GetQuestions())
                {
                    Console.WriteLine(q.ToString());

                    // if (q.GetType() == typeof(YesNoQuestion))
                    if (q is YesNoQuestion)
                    {
                        var qYesNo = (YesNoQuestion)q;
                        //qYesNo.
                        Console.WriteLine("Y/N");
                        string input = Console.ReadLine().ToUpper().Trim();
                        switch (input)
                        {
                            case "Y":
                                {
                                    qYesNo.Answer = true;
                                    break;
                                }
                            case "N":
                                {
                                    qYesNo.Answer = false;
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
                        var qScale = (ScaleQuestion)q;
                        int input = Convert.ToInt32(Console.ReadLine());        //lägg till try/catch
                        if (input > 0 && input < 6)
                        {
                            System.Console.WriteLine(" här är skalan 1-5!!!!");
                            System.Console.WriteLine(q.GetType().ToString());
                            qScale.Answer = input;
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


        public static void DisplaySurveyMenu(SurveyManager surveyManager)
        {
            while (true)
            {
                //   Console.WriteLine("What kind of Answer do you want?");
                //   Console.WriteLine("[Y]es or No Question\n[S]caleQuestion");
                //    string questionChoice = Console.ReadLine().ToUpper().Trim();     


                //   switch (questionChoice)
                //   {
                // case "S":
                // List<Question> listofquestion = new List<Question>();

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
                            surveyManager.TypeOfQuestion(input, "YesNo");
                            //listofquestion.Add(new YesNoQuestion(input));
                            break;
                        case "N":
                            surveyManager.TypeOfQuestion(input, "Scale");
                            // listofquestion.Add(new ScaleQuestion(input));
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
                        //foreachloop bara för att skriva ut alla frågor som skapats
                        foreach (var q in surveyManager.GetQuestions())
                        {
                            Console.WriteLine(q.ToString());
                        }
                    }
                    else if (continueInput == "Y")
                    {
                        isDone = false;
                    }
                }
                surveyManager.CreateNewSurvey(surveyManager.GetQuestions(), surveyName);
                break;
                // break;

                // case "Y":

                //     break;

                // default:
                // break;
            }

        }
    }
}
