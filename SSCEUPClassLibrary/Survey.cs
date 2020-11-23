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
    }
}
