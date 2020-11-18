namespace SSCEUPClassLibrary
{
    public abstract class Question
    {
        public int ID { get; set; }
        public string Text { get; set; }
         

        public Question()
        {

        }
        public Question(string text)
        {
            Text = text;
        }
     
    }

}
