namespace SSCEUPClassLibrary
{
    public class YesNoQuestion : Question
    {
        protected bool Answer { get; set; }
        public YesNoQuestion(string text) : base(text)
        {
            Text = text;
        }

        public override string ToString()
                {
                    return Text;
                }
    }
}
