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

        public int GetSurveyId(string title)
        {
            int surveyID = db.GetSurveyIdFromDB(title);

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

        public void InsertAwnsers(List<Question> listOFQuestions)
        {
            db.InsertIntoAnswer(listOFQuestions);
        }
    }
}
