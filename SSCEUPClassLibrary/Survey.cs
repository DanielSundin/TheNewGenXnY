using System;
using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class Survey
    {
        //private List<Question> questions = new List<Question>();

        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string SurveyCode { get; set; }

        public Survey()
        {

        }

        public Survey(string title)
        {
            this.Title = title;
        }

        public Survey(int surveyId,string title)
        {
            this.SurveyId=surveyId;
            this.Title=title;
        }

        // public override string ToString()          //kolla om behövs
        // {
        //     return $"{SurveyId} : {Title}";
        // }
    }
}
