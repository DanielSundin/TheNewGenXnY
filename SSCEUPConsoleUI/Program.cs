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
                System.Console.WriteLine("\tOptions\n[A]dd Survey\n[D]o Survey\n[L]ist Surveys\n[G]et Statistics\n[R]emove Survey\n[Q]uit");
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
                    case "L":
                        {
                            Console.Clear();
                            PrintSurveys(surveyManager);
                            break;
                        }
                    case "G":
                        {
                            Console.Clear();
                            PrintSurveys(surveyManager);
                            GetStatistics(surveyManager);
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
            Console.Clear();
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
                bool validChoice = false;
                System.Console.WriteLine($"Question {question.QuestionId.ToString()} :  {question.Text}");
                if (question.IsYesNoQuestion == true)
                {


                    while (!validChoice)
                    {

                        System.Console.WriteLine("\n [Y] or [N]");
                        string choice = Console.ReadLine().ToUpper().Trim();
                        if (choice == "Y")
                        {
                            answers.Add(new Answer(question.QuestionId, true));
                            validChoice = true;
                        }
                        else if (choice == "N")
                        {
                            answers.Add(new Answer(question.QuestionId, false));
                            validChoice = true;
                        }
                        else
                        {
                            System.Console.WriteLine("Not a valid option");
                            PressEnterToContinue();
                        }
                    }
                }

                else if (question.IsYesNoQuestion == false)
                {

                    foreach (var scaleChoice in Enum.GetValues(typeof(AnswerScale)))
                    {
                        System.Console.WriteLine($"{(int)scaleChoice}:  {scaleChoice}");
                    }

                    while (!validChoice)
                    {
                       int userInput=7;
                       try
                       {
                            userInput = Convert.ToInt32(Console.ReadLine());
                       }
                       catch (System.Exception e)
                       {
                            System.Console.WriteLine("Error! " + e.Message);
                            System.Console.WriteLine("Try again.");
                       }
                        if(userInput >0 && userInput <6)
                        {
                            answers.Add(new Answer(question.QuestionId, userInput));
                            validChoice = true;
                        }
                             
                    }
                }
            }
            surveyManager.InsertAnswers(answers);
            System.Console.WriteLine("Thank you for participating, have a nice day!");
            PressEnterToContinue();

        }

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
            int surveyid = surveyManager.GetSurveyId(surveyCode);

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

        public static int GetInt(string message)
        {
            string stringInput = "";
            int value = 0;
            bool flag = true;

            while (flag == true)
            {
                Console.WriteLine(message);
                stringInput = Console.ReadLine().Trim();
                try
                {
                    value = Convert.ToInt32(stringInput);
                    flag = false;
                }
                catch
                {
                    Console.WriteLine($"ERROR! {stringInput} is not a valid integer.");
                    flag = true;
                }
            }

            // Console.WriteLine(message);
            // while (!int.TryParse(stringInput, out value))
            // {
            //     Console.WriteLine($"{stringInput} is not a valid integer. put in a number.");
            // }

            return value;
        }
        
        public static void PrintSurveys(SurveyManager surveyManager)
        {
            foreach (var item in surveyManager.GetSurveys())
            {
                System.Console.WriteLine(item.SurveyId + " " + item.Title);
            }
        }

        public static void GetStatistics(SurveyManager surveyManager)
        {
            
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
// }