using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class SurveyManager
    {
      public List<Survey> listOfSurveys = new List<Survey>();

       
        Survey survey = new Survey();

        //public enum QuestionType{Scale,YesNo}

        // public QuestionType questiontype;
        // public void DoSomething()
        // {
        //     if (questiontype == QuestionType.Scale)
        //     {
        //         //QuestionType.valueOf(type)
        //     }
        // }
        public void TypeOfQuestion(string nameOfQuestion, string type)
        {
            if (type == "Scale")
            {
                survey.AddScaleQuestion(nameOfQuestion);
            }
            else if (type == "YesNo")
            {
                survey.AddYesNoQuestion(nameOfQuestion);
            }
        }

        public Survey CreateNewSurvey(List<Question> listOfQuestions, string name)
        {

            Survey newsurvey = new Survey(listOfQuestions, name);
            listOfSurveys.Add(newsurvey);
            return newsurvey;

        }

        public List<Survey> GetListOfSurveys()
        {
            // vill vara mellan länk till dbRepositoryt så koden här bara hänvisa till metoden i bdraspotoryt
            // lista alla surveys
            List<Survey> tempList = new List<Survey>();
            foreach (Survey survey in listOfSurveys)
            {
                tempList.Add(survey);
            }

            return tempList;

        }
        
        public List<Question> GetQuestions()
        {
            return survey.GetListOfQuestions();
        }

        public void SubmitSurvey()
        {
            // submitta survey
        }

        public void RemoveSurvey()
        {
            // ta bort survey
        }
    }

    
       

}