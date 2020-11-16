namespace SSCEUPClassLibrary
{
    public class ScaleQuestion : Question
    {
        public int Answer { get; set; }
        public ScaleQuestion(string text) : base(text)
        {
            this.Text = text;
        }
        public override string ToString()
        {
            return Text;
        }
        public ScaleQuestion(int answer) : base()
        {
            this.Answer = answer;
        }
    }

    // public enum ScaleAnswer  
    // {
    //     Horrible = 1,
    //     Bad,   
    //     Neutral,
    //     Good,     
    //     Great
    // }
}