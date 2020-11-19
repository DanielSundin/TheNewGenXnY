namespace SSCEUPClassLibrary
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int SurveyId { get; set; }
        public bool IsYesNoQuestion { get; set; }
        public string Text { get; set; }

        public Question()
        {
            
        }
         

        public Question(int surveyId, bool isYesNoQuestion,string text)
        {
            this.SurveyId=surveyId;
            this.IsYesNoQuestion = isYesNoQuestion;
            this.Text=text;
        }
        public Question(string text)
        {
            Text = text;
        }
     
    }

}
