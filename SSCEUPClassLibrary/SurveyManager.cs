using System.Collections.Generic;

namespace SSCEUP
{
    class SurveyManager
    {
        List<Survey> listOfSurveys = new List<Survey>();
        List<User> ListOfUsers = new List<User>();

        public void CreateNewSurvey()
        {
            //skapa survey
        }
        
        public List<Survey> GetListOfSurveys()
        {
            // lista alla surveys
            List<Survey> tempList = new List<Survey>();
            foreach (Survey survey in listOfSurveys)
            {
                tempList.Add(new Survey(survey));
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