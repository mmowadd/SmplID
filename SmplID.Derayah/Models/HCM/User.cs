namespace SmplID.Derayah.Models.HCM
{
    public class User
    {
        public ulong UserId { get; set; }
        public string Username { get; set; }
        public ulong PersonId { get; set; }
        public string PersonNumber { get; set; }
        // public HCMUserName name { get; set; }
        //public string active { get; set; }


    }
    public class UserName
    {
        public string givenName { get; set; }
        public string familyName { get; set; }
    }
}
