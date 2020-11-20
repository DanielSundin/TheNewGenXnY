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
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine("The input parameter(username or password) was really strange, and therefore error." + e);
                    Console.ReadKey();
                    Console.WriteLine(e);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine($"You didn't fill in any information!");
                    Console.ReadKey();
                    Console.WriteLine(e);
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    Console.WriteLine("There is something off with the database. I can't really see what from here..");
                    Console.ReadKey();
                    Console.WriteLine(e);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Something went wrong...press key to read error message");
                    Console.ReadKey();
                    Console.WriteLine(e);
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
                Console.Clear();
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
                    default:
                        Console.WriteLine("Returning you to menu - try choosing a valid menu option");
                        RunAdminMode(surveyManager);
                        return;
                }
            }
        }
        private static void DoSurvey(SurveyManager surveyManager)
        {
            string surveyCode = "";
            bool isCodeNotFound = true;
            while (isCodeNotFound)
            {
                Console.Clear();
                System.Console.WriteLine("Input given surveycode:");
                surveyCode = Console.ReadLine();
                bool exists = surveyManager.CheckSurveyCode(surveyCode);
                if (exists == true)
                {
                    isCodeNotFound = false;
                }
                else
                {
                    System.Console.WriteLine("Code not found");
                    PressEnterToContinue();
                    return;
                }

            }

            List<Question> ListOfquestions = surveyManager.GetSurveyWithQuestions(surveyCode);
            List<Answer> answers = new List<Answer>();
            foreach (var question in ListOfquestions)
            {
                System.Console.WriteLine($"Question {question.QuestionId.ToString()} :  {question.Text}");
                if (question.IsYesNoQuestion == true)
                {
                    System.Console.WriteLine("\n [Y] or [N]");
                    string choice = Console.ReadLine();
                    while (true)
                    {
                        switch (choice)
                        {
                            case "Y":
                                answers.Add(new Answer(question.QuestionId, true));
                                break;
                            case "N":
                                answers.Add(new Answer(question.QuestionId, false));
                                break;
                        }
                    }
                }

                else if (question.IsYesNoQuestion == false)
                {
                    foreach (var scaleChoice in Enum.GetValues(typeof(AnswerScale)))
                    {
                        System.Console.WriteLine($"{(int)scaleChoice}:  {scaleChoice}");
                        int anotherinput = Convert.ToInt32(Console.ReadLine());
                       //int validate
                       
                        answers.Add(new Answer(question.QuestionId, anotherinput));
                    }
                }
                surveyManager.InsertAnswers(answers);
                PressEnterToContinue();
            }

        }





        // foreach (var item in surveyManager.GetAllSurveys())
        // {
        //     System.Console.WriteLine(item.Title);
        // }
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




        public static void CreateSurvey(SurveyManager surveyManager)
        {

            Console.WriteLine("What do you want to name the survey?");
            string surveyName = Console.ReadLine();
            if (string.IsNullOrEmpty(surveyName) == true)
            {
                Console.WriteLine("The survey name cannot be blank.");
                PressEnterToContinue();
                return;
            }
            Console.WriteLine("Survey Code?");
            string surveyCode = Console.ReadLine();
            if (string.IsNullOrEmpty(surveyCode) == true)
            {
                Console.WriteLine("The survey code cannot be blank.");
                PressEnterToContinue();
                return;
            }

            surveyManager.SaveSurveyName(surveyName, surveyCode);
            List<Question> questions = new List<Question>();
            int surveyid = surveyManager.GetSurveyId(surveyName);

            bool isDone = false;
            while (isDone == false)
            {
                Console.WriteLine("What is the question?");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) == true)
                {
                    Console.WriteLine("The question cannot be blank.");
                    PressEnterToContinue();
                    return;
                }
                Console.WriteLine("Is this a Yes/No Question? No will make the question a scaled question.\n(Y/N)\n");
                string choice = Console.ReadLine().ToUpper().Trim();

                switch (choice)
                {
                    case "Y":
                        questions.Add(new Question(surveyid, true, input));
                        break;
                    case "N":
                        questions.Add(new Question(surveyid, false, input));
                        break;
                    default:
                        Console.WriteLine("If you dont know if this is a yes or no question or not, maybe you should rephrase the question!");
                        PressEnterToContinue();
                        continue;
                }
                Console.Clear();
                Console.WriteLine("Add more Questions? Y/N");
                string continueInput = Console.ReadLine().ToUpper().Trim();
                if (continueInput == "N")
                {
                    isDone = true;
                    try
                    {
                        surveyManager.SavetoDB(questions);
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        Console.WriteLine("There is something off with the database. I can't really see what from here..");
                        Console.ReadKey();
                        Console.WriteLine(e);
                        return;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Something went wrong...press key to read error message");
                        Console.ReadKey();
                        Console.WriteLine(e);
                        return;
                    }
                }
                else if (continueInput == "Y")
                {
                    isDone = false;
                }
                else
                {
                    Console.WriteLine("Does that mean NO? Please confirm (Y/N)");
                    isDone = false;
                    return;
                }
            }
        }

        private static void PressEnterToContinue()
        {
            Console.WriteLine("\nPress ENTER to continue.");
            Console.ReadLine();
            Console.Clear();
        }

        public enum AnswerScale
        {
            Horrible = 1,
            Bad,
            Neutral,
            Good,
            Great
        }

        // private string Validate(string input)
        // {

        // try
        // {

        // }
        // catch (IndexOutOfRangeException e)
        // {
        //     Console.WriteLine("The input parameter was really strange, and therefore error." + e);
        //     Console.ReadKey();
        //     Console.WriteLine(e);
        // }
        // catch (ArgumentNullException e)
        // {
        //     Console.WriteLine($"You didn't fill in any information!");
        //     Console.ReadKey();
        //     Console.WriteLine(e);
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine("Something went wrong...press key to read error message");
        //     Console.ReadKey();
        //     Console.WriteLine(e);
        // }
        //  }
    }
}
