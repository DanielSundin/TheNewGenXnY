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

        public List<Survey> GetAllSurveys()
        {
           List<Survey> allSurveys = new List <Survey>(db.GetSurveysFromDB());
           return allSurveys;
        }

        public int GetSurveyId(string title)
        {
            int surveyID = db.GetSurveyIdFromDB(title);
            
            return surveyID;
          //  return temp.SurveyId; //tempreturn tills metod är klar
        }



        // public Survey CreateNewSurvey(List<Question> listOfQuestions, string name)
        // {

        //     // Survey newsurvey = new Survey(listOfQuestions, name);
        //     // listOfSurveys.Add(newsurvey);
        //     return;

        // }

        // public List<Survey> GetListOfSurveys()
        // {
        //     // vill vara mellan länk till dbRepositoryt så koden här bara hänvisa till metoden i bdraspotoryt
        //     // lista alla surveys
        //     List<Survey> tempList = new List<Survey>();
        //     foreach (Survey survey in listOfSurveys)
        //     {
        //         tempList.Add(survey);
        //     }

        //     return tempList;

        // }

        // public List<Question> GetQuestions()
        // {
        //     return survey.GetListOfQuestions();
        // }  
    }




}