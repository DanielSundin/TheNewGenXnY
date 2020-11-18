using System;
using System.Collections.Generic;

namespace SSCEUPClassLibrary
{
    public class Survey
    {

       
        private List<Question> questions = new List<Question>();

        public int Id { get; set; }
        public string Name { get; set; }

        public Survey()
        {
            
        }
        public Survey(List<Question> questions, string name)
        {
            this.questions = questions;
            this.Name = name;
        }


        public void AddScaleQuestion(string nameOfQuestion)
        {
            Question question = new ScaleQuestion(nameOfQuestion);
            questions.Add(question);
        }

        public void AddYesNoQuestion(string nameOfQuestion)
        {
            Question question = new YesNoQuestion(nameOfQuestion);
            questions.Add(question);
        }

       

        public List<Question> GetListOfQuestions()
        {
            List<Question> tempListOfQuestions = new List<Question>();
            foreach (Question q in questions)
            {
               tempListOfQuestions.Add(q);
            }
            return tempListOfQuestions;
        }
    }
}
