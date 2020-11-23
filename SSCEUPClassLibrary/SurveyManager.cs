using System.Collections.Generic;
using System.Linq;

namespace SSCEUPClassLibrary
{
    public class SurveyManager
    {
        DBhandler db = new DBhandler("Server=40.85.84.155;Database=OOPGroup4;User=Student22;Password=zombie-virus@2020;");

        public void SavetoDB(List<Question> list)
        {
            db.InsertIntoQuestion(list);
        }

        public void SaveSurveyName(string title, string surveyCode)
        {
            Survey survey = new Survey(title, surveyCode);
            db.InsertIntoSurvey(survey);
        }

        public List<Question> GetSurveyWithQuestions(string surveyCode)
        {
            List<Question> allSurveys = new List<Question>(db.GetSurveyQuestionsFromDB(surveyCode));
            return allSurveys;
        }

        public int GetSurveyId(string surveyCode)
        {
            int surveyID = db.GetSurveyIdFromDB(surveyCode);
            return surveyID;
        }

        public bool CheckSurveyCode(string input)
        {
            Survey survey = db.GetSurveyCodeFromDB(input).FirstOrDefault();
            if (survey == null)
            {
                return false;
            }
            return true;
        }

        public void InsertAnswers(List<Answer> listOfAnswers)
        {
            db.InsertIntoAnswer(listOfAnswers);
        }

        public List<Statistic> GetStatistic(int surveyId)
        {
            List<Statistic> surveyStatistics = new List<Statistic>(db.GetStatisticFromDB(surveyId));
            return surveyStatistics;
        }

        public List<Survey> GetSurveys()
        {
            List<Survey> surveys = new List<Survey>(db.GetSurveysFromDB());
            return surveys;
        }

        public string GetSurveyTitle(string surveyCode)
        {
            Survey survey = db.GetSurveyCodeFromDB(surveyCode).FirstOrDefault();
            return survey.Title;
        }

        public int GetUserId(string userName)
        {
            List<User> tempUser = new List<User>(db.GetUser(userName));
            return tempUser[0].UserId;
        }

        public void InsertUserIdAndSurveyIdToUserSurvey(int userId, int surveyId)
        {
            db.InsertIntoUserSurvey(userId, surveyId);
        }

        public bool CheckUIdAndSIdAgainstDB(int userId, int surveyId)
        {
            List<User_Survey> validateInfo = new List<User_Survey>(db.CheckUserIDAndSurveyIdInDB(userId, surveyId));
            if (validateInfo.Count == 0)
            {
                return false;
            }
            if (userId == validateInfo[0].UserId && surveyId == validateInfo[0].SurveyId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
