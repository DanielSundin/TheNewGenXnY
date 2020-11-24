namespace SSCEUPClassLibrary
{
    internal class User
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public string UserPass { get; private set; }
        public bool IsAdmin { get; private set; }
    }
}