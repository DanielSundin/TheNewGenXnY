using System;
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

        public string GetSurvey(string surveyCode)
        {
            Survey survey = db.GetSurveyCodeFromDB(surveyCode).FirstOrDefault();
            return survey.Title;
        }

        // public Survey GetSurveyTitle(string surveyCode)
        // {
        //     Survey surveyTitle = db.GetSurveyTitleFromDB();
        //     return surveyTitle
        // }
    }
}
