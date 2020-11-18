using System;
using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class Survey
    {
        //private List<Question> questions = new List<Question>();

        public int Id { get; set; }
        public string Title { get; set; }
        public string SurveyCode { get; set; }

        public Survey()
        {

        }

        public Survey(string title)
        {
            this.Title = title;
        }

        public Survey(int id,string title)
        {
            this.Id=id;
            this.Title=title;
        }

        public override string ToString()
        {
            return $"{Id} : {Title}";
        }
    }
}
