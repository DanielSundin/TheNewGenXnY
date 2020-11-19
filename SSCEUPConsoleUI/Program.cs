using System;
using System.Collections.Generic;
using SSCEUPClassLibrary;


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
            SurveyManager surveyManager = new SurveyManager();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\nLOGIN PROMPT\n");
            Console.ResetColor();

            for (int loginAttempts = 1; loginAttempts <= 3; loginAttempts++)
            {
                System.Console.WriteLine("Enter Username");
                string inputName = Console.ReadLine().ToLower();
                System.Console.WriteLine("Enter Password");
                string inputPass = Console.ReadLine().ToLower();
                try
                {
                    loginauth.CheckLoginInfo(inputName, inputPass);
                }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine("The input parameter(username or password) was really strange, and therefore error."+ e);
                }
                catch(ArgumentNullException e)
                {
                    Console.WriteLine($"You didn't fill in any information! ({e})");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong.."+e);
                }

                if (loginauth.CheckLoginInfo(inputName, inputPass) == 1)
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
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 2)
                {
                    RunUserMode(surveyManager);
                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 3)
                {
                    RunAdminMode(surveyManager);
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
                        {
                            Console.Clear();
                            CreateSurvey(surveyManager);
                            break;
                        }
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
                }
            }
        }
        private static void DoSurvey(SurveyManager surveyManager)
        {
            Console.Clear();
            foreach (var item in surveyManager.GetAllSurveys())
            {
                System.Console.WriteLine(item.Title);
            }

            System.Console.WriteLine("\nWhich one do you want to do ?");
            string iwannadothissurvey = Console.ReadLine();
            //      while (true)
            //     {
            //         // if (q.GetType() == typeof(YesNoQuestion))
            //         if (q is YesNoQuestion)
            //         {
            //             var qYesNo = (YesNoQuestion)q;
            //             //qYesNo.
            //             Console.WriteLine("Y/N");
            //             string input = Console.ReadLine().ToUpper().Trim();
            //             switch (input)
            //             {
            //                 case "Y":
            //                     {
            //                         qYesNo.Answer = true;
            //                         break;
            //                     }
            //                 case "N":
            //                     {
            //                         qYesNo.Answer = false;
            //                         break;
            //                     }
            //                 default:
            //                     {
            //                         Console.WriteLine("Choose Y or N.");
            //                         return;
            //                     }
            //             }
            //         }
            //         else
            //         {
            //             Console.WriteLine("(1-5)");
            //             var qScale = (ScaleQuestion)q;
            //             int input = Convert.ToInt32(Console.ReadLine());        //lägg till try/catch
            //             if (input > 0 && input < 6)
            //             {
            //                 System.Console.WriteLine(" här är skalan 1-5!!!!");
            //                 System.Console.WriteLine(q.GetType().ToString());
            //                 qScale.Answer = input;
            //             }
            //             else
            //             {
            //                 Console.WriteLine("Answer out of range!");
            //                 return;
            //             }
            //         }

            //     }
        }



        public static void CreateSurvey(SurveyManager surveyManager)
        {

            Console.WriteLine("What do you want to name the survey?");
            string surveyName = Console.ReadLine();
            Console.WriteLine("Survey Code?");
            string surveyCode = Console.ReadLine();
            surveyManager.SaveSurveyName(surveyName, surveyCode);
            List<Question> questions = new List<Question>();
            int surveyid = surveyManager.GetSurveyId(surveyName);
         
            bool isDone = false;
            while (isDone == false)
            {
                Console.WriteLine("Question?");
                string input = Console.ReadLine();
                Console.WriteLine("Is this a Yes/No Question? No will make the question a scaled question.\n(Y/N)\n");
                string choice = Console.ReadLine().ToUpper().Trim();

                switch (choice)
                {
                    case "Y":
                        // kör samma metod get surveyi (SendIDtoDB(surveyid))
                        questions.Add(new YesNoQuestion(surveyid, true, input));
                        break;
                    case "N":
                        questions.Add(new ScaleQuestion(surveyid, false, input));
                        break;
                    default:
                        Console.WriteLine("Sorry m8, (Y)es or (N)o only..please");
                        return;
                }

                Console.WriteLine("Add more Questions? Y/N");
                string continueInput = Console.ReadLine().ToUpper().Trim();
                if (continueInput == "N")
                {
                    isDone = true;
                    surveyManager.SavetoDB(questions);
                }
                else if (continueInput == "Y")
                {
                    isDone = false;
                }
            }
        }
    }
}
