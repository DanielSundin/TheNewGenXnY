namespace SSCEUPClassLibrary
{
    public class Survey
    {
        public int SurveyId { get; set; }
        public string Title { get; set; }
        public string SurveyCode { get; set; }

        public Survey(){}

        public Survey(string title, string surveyCode)
        {
            this.Title = title;
            this.SurveyCode = surveyCode;
        }

        public Survey(int surveyId, string title)
        {
            this.SurveyId = surveyId;
            this.Title = title;
        }
        public static Question CreateQuestion(int surveyId, bool YorN, string Text)
        {
            return new Question(surveyId, YorN, Text);
        }
        public static Answer CreateYorNAnswer(int questionId, bool YorN)
        {
            return new Answer(questionId, YorN);
        }
              public static Answer CreateScaleAnswer(int questionId, int userinput)
        {
            return new Answer(questionId, userinput);
        }
    }
}
