namespace SSCEUPClassLibrary
{
    public class Statistic
    {
        public string Title { get; private set; }
        public string Text { get; private set; }
        public bool IsYesNoQuestion { get; private set; }
        public int AVG { get; private set; }
        public int MAX { get; private set; }
        public int MIN { get; private set; }
        public int YESPROCENT { get; private set; }
    }
}