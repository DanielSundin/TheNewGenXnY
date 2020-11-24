namespace SSCEUPClassLibrary
{
    public class Answer
    {
        public int QuestionId { get; private set; }

        public int ScaleAnswer { get; private set; }

        public bool YoNAnswer { get; private set; }

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
    }
}