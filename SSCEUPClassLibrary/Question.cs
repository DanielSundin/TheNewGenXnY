namespace SSCEUPClassLibrary
{
    public abstract class Question
    {
       // protected int ID { get; set; }
        protected string Text { get; set; }  

        protected Question(string text)
        {
            Text = text;
        }
        public override string ToString()
                {
                    return Text;
                }
    }

}
