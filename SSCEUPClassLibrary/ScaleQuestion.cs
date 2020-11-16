namespace SSCEUPClassLibrary
{
    public class ScaleQuestion : Question
    {
        // public enum Answer { get; set; }
        
        public ScaleQuestion(string text) : base(text)
        {
           this.Text = text; 
        }
                public override string ToString()
                {
                    return Text;
                }
    }

    public enum ScaleAnswer  
    {
        Horrible = 1,
        Bad,   
        Neutral,
        Good,     
        Great
    }
}