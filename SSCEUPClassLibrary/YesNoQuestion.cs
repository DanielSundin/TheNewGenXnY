namespace SSCEUPClassLibrary
{
    public class YesNoQuestion : Question
    {
        public bool Answer { get; set; }
        public YesNoQuestion(string text) : base(text)
        {
            Text = text;
        }

        public YesNoQuestion(bool answer) : base()
        {
            this.Answer = answer;
        }

        public override string ToString()
                {
                    return Text + " " +Answer;
                }
    }
}
