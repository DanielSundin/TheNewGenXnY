namespace SSCEUPClassLibrary
{
    public class Answer
    {
        public int QuestionId { get; set; }

        public int ScaleAnswer { get; set; }

        public bool YoNAnswer { get; set; }

        public Answer(int questionId, bool yoNAnswer)
        {
            this.YoNAnswer = yoNAnswer;
            this.QuestionId = questionId;
        }

        public Answer(int questionId, int scaleAnswer)
        {
            this.ScaleAnswer = scaleAnswer;
            this.QuestionId = questionId;
        }

        public Answer(int questionId, int scaleAnswer, bool yoNAnswer)
        {
            this.ScaleAnswer = scaleAnswer;
            this.QuestionId = questionId;
            this.YoNAnswer = yoNAnswer;
        }
    }
}