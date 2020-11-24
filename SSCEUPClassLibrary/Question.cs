namespace SSCEUPClassLibrary
{
    public class Question
    {
        public int QuestionId { get; private set; }
        public int SurveyId { get; private set; }
        public bool IsYesNoQuestion { get; private set; }
        public string Text { get; private set; }

        public Question() {}
        internal Question(int surveyId, bool isYesNoQuestion, string text)
        {
            this.SurveyId = surveyId;
            this.IsYesNoQuestion = isYesNoQuestion;
            this.Text = text;
        }

    }
}
