using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class SurveyManager
    {
        List<Survey> listOfSurveys = new List<Survey>();
        List<User> ListOfUsers = new List<User>();

        public Survey CreateNewSurvey(List<Question> listOfQuestions, string name)
        {

            Survey newsurvey = new Survey(listOfQuestions, name);
            return newsurvey;

        }

        public List<Survey> GetListOfSurveys()
        {
            // vill vara mellan länk till dbRaspotoryt så koden här bara hänvisa till metoden i bdraspotoryt
            // lista alla surveys
            List<Survey> tempList = new List<Survey>();
            foreach (Survey survey in listOfSurveys)
            {
                tempList.Add(survey);
            }

            return tempList;

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