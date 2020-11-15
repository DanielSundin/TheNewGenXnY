namespace SSCEUP
{
    internal class ScaleQuestion : Question
    {
        // public enum Answer { get; set; }
        
        internal ScaleQuestion(string text) : base(text)
        {
           this.Text = text; 
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