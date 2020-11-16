using System;
using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class Survey
    {

        // private List<Survey> listOfSurveys = new List<Survey>();
        private List<Question> Questions = new List<Question>();
        private List<Question> SampleSurvey = new List<Question>();
        
        // private Survey survey;

        
        public string Name { get; set; }
        public int Id { get; set; }

        //fråga Thomas om denna.
        public Survey()
        {}
        // public Survey(Survey survey)  Fråga Thomas
        // {
        //     this.survey = survey;
        // }
        public Survey(List<Question> questions, string name)
        {
            this.Questions = questions;
            this.Name = name;
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
        public void MakeSampleList()
        {
           // List<Question> SampleSurvey = new List<Question>();
            SampleSurvey.Add(CreateScaleQuestion("Hur bra är röd på en skala 1-5"));
            SampleSurvey.Add(CreateScaleQuestion("Hur bra är grön på en skala 1-5"));
            SampleSurvey.Add(CreateScaleQuestion("Hur bra är gul på en skala 1-5"));
            
        }
    }
}
