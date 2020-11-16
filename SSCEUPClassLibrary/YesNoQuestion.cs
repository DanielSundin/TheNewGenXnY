namespace SSCEUPClassLibrary
{
    public class YesNoQuestion : Question
    {
        protected bool Answer { get; set; }
        public YesNoQuestion(string text) : base(text)
        {
            Text = text;
        }

        public YesNoQuestion(bool answer) : base()
        {
            Answer = answer;
        }

        public override string ToString()
                {
                    return Text;
                }
    }
}
