using System;
using System.Collections.Generic;
using System.Threading;
using Figgle;
using SSCEUPClassLibrary;



namespace SSCEUP
{
    class Program
    {

        static void Main(string[] args)
        {
            StartupMessage();
            RunLogin();
        }
        private static string currentUser = "";

        public static string CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        private static void RunLogin()
        {   
            LoginAuthentication loginauth = new LoginAuthentication();
            SurveyManager surveyManager = new SurveyManager();
            ColorTheText("green","\nLOGIN PROMPT\n");
            for (int loginAttempts = 1; loginAttempts <= 3; loginAttempts++)
            {
                ColorTheText("cyan","Enter Username\n");
                string inputName = Console.ReadLine().ToLower();
                ColorTheText("cyan","Enter Password\n");
                string inputPass = Console.ReadLine().ToLower();
                loginauth.CheckLoginInfo(inputName, inputPass);

                if (loginauth.CheckLoginInfo(inputName, inputPass) == 1 || inputName == null)
                {
                    ColorTheText("red","Username or Password was incorrect");
                    PressEnterToContinue();
                    if (loginAttempts >= 3)
                    {
                        Console.Clear();
                        ColorTheText("red","Too many attempts,try again later.");
                        Environment.Exit(0);
                    }

                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 2)
                {
                    CurrentUser = inputName;
                    RunUserMode(surveyManager);
                }
                else if (loginauth.CheckLoginInfo(inputName, inputPass) == 3)
                {
                    CurrentUser = inputName;
                    RunAdminMode(surveyManager);
                }
            }
        }

        private static void RunUserMode(SurveyManager surveyManager)
        {
            Dictionary<string, string> answerScale = DefineAnswerScaleValues();

            while (true)
            {
                // user menu
                Console.Clear();
                ColorTheText("green","\tOptions\n[D]o Survey\n[Q]uit\n");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "D":
                        {
                            Console.Clear();
                            DoSurvey(surveyManager, answerScale);
                            break;
                        }
                    case "Q":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            ColorTheText("red","Invalid Choice!\n");
                            PressEnterToContinue();
                            break;
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
                ColorTheText("green","\tOptions\n[A]dd Survey\n[L]ist Surveys\n[G]et Statistics\n[Q]uit\n");
                string input = Console.ReadLine().ToUpper();
                switch (input)
                {
                    case "A":
                        {
                            Console.Clear();
                            CreateSurvey(surveyManager);
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
                        ColorTheText("red","Returning you to menu - try choosing a valid menu option\n");
                        RunAdminMode(surveyManager);
                        return;
                }
            }
        }
        private static void DoSurvey(SurveyManager surveyManager, Dictionary<string, string> answerScale)
        {
            Console.Clear();
            string surveyCode = "";
            bool isCodeFound = false;
            while (!isCodeFound)
            {
                Console.Clear();
                ColorTheText("green","Input given surveycode: \n");
                surveyCode = Console.ReadLine();
                bool exists = surveyManager.CheckSurveyCode(surveyCode);
                if (exists == true)
                {

                    if (surveyManager.CheckUIdAndSIdAgainstDB(surveyManager.GetUserId(currentUser), surveyManager.GetSurveyId(surveyCode)) == false)
                    {
                        Console.Clear();
                        isCodeFound = true;
                        ColorTheText("green", "Survey name: "+surveyManager.GetSurveyTitle(surveyCode));
                        PressEnterToContinue();
                    }
                    else
                    {
                        System.Console.WriteLine("You have already done this survey.");
                        PressEnterToContinue();
                        return;
                    }

                }
                else
                {
                    ColorTheText("red", "Code not found\n");
                    PressEnterToContinue();
                    return;
                }

            }
            Console.Clear();
            List<Question> ListOfquestions = surveyManager.GetSurveyWithQuestions(surveyCode);
            List<Answer> answers = new List<Answer>();
            int QuestionCounter = 1;
            foreach (var question in ListOfquestions)
            {
                bool validChoice = false;
                if (question.IsYesNoQuestion == true)
                {
                    Console.Clear();
                    while (!validChoice)
                    {
                        ColorTheText("cyan",$"Question {QuestionCounter.ToString()} :  {question.Text}\n\n");
                        ColorTheText("green","\n [Y] or [N]\n\n");
                        ColorTheText("green","Answer: ");
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
                            ColorTheText("red","Not a valid option");
                            PressEnterToContinue();
                        }
                    }
                }

                else if (question.IsYesNoQuestion == false)
                {

                    Console.Clear();
                    while (!validChoice)
                    {
                        ColorTheText("cyan",$"Question {QuestionCounter.ToString()} :  {question.Text}\n\n");
                        PrintAnswerScale(answerScale);
                        int userInput = 0;
                        try
                        {
                            userInput = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            // System.Console.WriteLine("Error! " + e.Message);
                        }
                        if (userInput > 0 && userInput < 6)
                        {
                            answers.Add(new Answer(question.QuestionId, userInput));
                            validChoice = true;
                        }
                        else
                        {
                            Console.Clear();
                            ColorTheText("red","Input should preferably be a number and between 1-5");
                            PressEnterToContinue();
                        }

                    }
                }
                QuestionCounter++;
            }
            Console.Clear();
            surveyManager.InsertUserIdAndSurveyIdToUserSurvey(surveyManager.GetUserId(currentUser), surveyManager.GetSurveyId(surveyCode));
            surveyManager.InsertAnswers(answers);
            ColorTheText("cyan","Thank you for participating, have a nice day!\n");
            PressEnterToContinue();

        }

        public static void CreateSurvey(SurveyManager surveyManager)
        {
            ColorTheText("green","What do you want to name the survey?\n");
            string surveyName = Console.ReadLine();
            if (string.IsNullOrEmpty(surveyName) == true)
            {
                ColorTheText("red","The survey name cannot be blank.");
                PressEnterToContinue();
                return;
            }
            ColorTheText("green","Survey Code?\n");
            string surveyCode = Console.ReadLine();
            if (string.IsNullOrEmpty(surveyCode) == true)
            {
                ColorTheText("red", "The survey code cannot be blank.");
                PressEnterToContinue();
                return;
            }

            surveyManager.SaveSurveyName(surveyName, surveyCode);
            List<Question> questions = new List<Question>();
            int surveyid = surveyManager.GetSurveyId(surveyCode);

            bool isDone = false;
            while (isDone == false)
            {
                ColorTheText("green","What is the question?\n");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input) == true)
                {
                    ColorTheText("red","The question cannot be blank.");
                    PressEnterToContinue();
                    return;
                }
                ColorTheText("green","Is this a Yes/No Question? No will make the question a scaled question.\n(Y/N)\n");
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
                        ColorTheText("red","If you dont know if this is a yes or no question or not, maybe you should rephrase the question!");
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
            ColorTheText("blue","\nPress ENTER to continue.");
            Console.ReadLine();
            Console.Clear();
        }


        public static void PrintSurveys(SurveyManager surveyManager)
        {
            Console.ForegroundColor=ConsoleColor.Green;
            foreach (var item in surveyManager.GetSurveys())
            {
                System.Console.WriteLine($"ID: {item.SurveyId}, Name: {item.Title}, Code: {item.SurveyCode}");
            }
            ColorTheText("blue","\nPress ENTER to continue.");
            Console.ReadLine();
        }


        public static Dictionary<string, string> DefineAnswerScaleValues()
        {
            Dictionary<string, string> answerScale = new Dictionary<string, string>();
            answerScale.Add("1", "Strongly Disagree ");
            answerScale.Add("2", "Disagree ");
            answerScale.Add("3", "Undecided ");
            answerScale.Add("4", "Agree ");
            answerScale.Add("5", "Strongly Agree ");
            return answerScale;
        }

        public static void PrintAnswerScale(Dictionary<string, string> answerScale)
        {
            Console.ForegroundColor=ConsoleColor.Green;
            foreach (var item in answerScale)
            {
                Console.Write("[{0}]{1} ", item.Key, item.Value);
            }
            System.Console.Write("\n\nAnswer: ");
            Console.ResetColor();
        }


        public static void GetStatistics(SurveyManager surveyManager)
        {
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("Welcome to Statistics");
            while (true)
            {
                int surveyId = ValidateInt("Input Survey ID:");
                List<Statistic> listOfStatistics = surveyManager.GetStatistic(surveyId);
                Console.WriteLine("Survey: " + listOfStatistics[surveyId - 1].Title);

                if (surveyId <= listOfStatistics.Count + 1 && surveyId > 0)
                {
                    foreach (var item in listOfStatistics)
                    {
                        if (item.IsYesNoQuestion == true)
                        {
                            Console.WriteLine($"Question: {item.Text} \nYes in Procent {item.YESPROCENT}");
                        }
                        else
                        {
                            Console.WriteLine($"Question: {item.Text} \nAVG: {item.AVG} \nMAX: {item.MAX} \nMIN: {item.MIN}");
                        }
                    }
                    PressEnterToContinue();
                    break;
                }
                else
                {
                    ColorTheText("red","Invalid Survey Id.");
                    PressEnterToContinue();
                }
            }
        }


        public static int ValidateInt(string message)
        {
            int intToValidate = 0;
            bool isNotConverted = true;
            while (isNotConverted)
            {
                Console.WriteLine(message);
                try
                {
                    intToValidate = Convert.ToInt32(Console.ReadLine());
                    isNotConverted = false;
                }
                catch (Exception e)
                {
                    ColorTheText("red","Error! " + e.Message);
                    ColorTheText("red","Try again.");
                }
            }
            return intToValidate;
        }

        public static void StartupMessage()
        {
            Console.Clear();
            ColorTheAscII("red","GenXnY");

            List<string> presentList = new List<string>()
             { 
              "P","r","o","u","d","l","y",
              " ",
              "P", "r", "e", "s", "e", "n", "t", "s",
              ".",".",".",
             };

            List<string> ssceupList = new List<string>()
             {
              "S","t","a","t","e","n","s",
              " ",
              "S", "t", "a", "t", "i", "s", "t", "i","s","k","a",
              " ",
              "C","e","n","t","r","a","l","b","y","r","å","s",
              " ",
              "E","l","e","k","t","r","o","n","i","s","k","a",
              " ",
              "U","n","d","e","r","s","ö","k","n","i","n","g","s","p","l","a","t","t","f","o","r","m",
             };

            foreach (var item in presentList)
            {
                Thread.Sleep(100);
                ColorTheText("green",item);
            }
            Thread.Sleep(2500);
            
            Console.Clear();
            ColorTheAscII("green","SSCEUP");
            Thread.Sleep(250);
            Console.Clear();
            ColorTheAscII("red", "SSCEUP");
            Thread.Sleep(250);
            Console.Clear();
            ColorTheAscII("cyan", "SSCEUP");
            Thread.Sleep(250);
            Console.Clear();
            ColorTheAscII("blue", "SSCEUP");
            Thread.Sleep(250);

            foreach (var item in ssceupList)
            {
                Thread.Sleep(100);
                ColorTheText("green", item);
            }
            PressEnterToContinue();
        }

        private static void ColorTheText(string color ,string text)
        {
            switch (color.ToLower())
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                default:
                    break;
            }
            Console.Write($"{text}");
            Console.ResetColor();
        }

        private static void ColorTheAscII(string color, string text)
        {
            switch (color.ToLower())
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;

                case "cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;

                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case "magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                default:
                    break;
            }
            Console.WriteLine(FiggleFonts.Slant.Render(text));
            Console.ResetColor();
        }

    }
}
