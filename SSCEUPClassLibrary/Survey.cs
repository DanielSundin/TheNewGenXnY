namespace SSCEUPClassLibrary
{
    public class Survey
    {
        public int SurveyId { get; private set; }
        public string Title { get; private set; }
        public string SurveyCode { get; private set; }

        internal Survey(string title, string surveyCode)
        {
            this.Title = title;
            this.SurveyCode = surveyCode;
        }

        internal static Question CreateQuestion(int surveyId, bool YorN, string Text)
        {
            return new Question(surveyId, YorN, Text);
        }

        internal static Answer CreateYorNAnswer(int questionId, bool YorN)
        {
            return new Answer(questionId, YorN);
        }

        internal static Answer CreateScaleAnswer(int questionId, int userinput)
        {
            return new Answer(questionId, userinput);
        }
    }
}
