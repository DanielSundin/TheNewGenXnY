using System;
using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class Survey
    {

        // private List<Survey> listOfSurveys = new List<Survey>();
        private List<Question> Questions = new List<Question>();
        private Survey survey;

        public Survey(Survey survey)
        {
            this.survey = survey;
        }



        /* TODO
        * Lägga till metoder som passar här.  
        *
        *
        */
        internal ScaleQuestion CreateScaleQuestion(string inputQuestion)
        {
            ScaleQuestion scaleQuestion = new ScaleQuestion(inputQuestion);

            return scaleQuestion;
        }
        internal YesNoQuestion CreateYesNoQuestion(string inputQuestion)
        {
            YesNoQuestion yesNoQuestion = new YesNoQuestion(inputQuestion);

            return yesNoQuestion;
        }
    }
}
