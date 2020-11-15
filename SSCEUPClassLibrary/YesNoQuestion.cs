namespace SSCEUP
{
    internal class YesNoQuestion : Question
    {
        protected bool Answer { get; set; }
        public YesNoQuestion(string text) : base(text)
        {
            Text = text;
        }
    }
}
