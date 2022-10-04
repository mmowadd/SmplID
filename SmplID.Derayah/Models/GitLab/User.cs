using System.Collections.Generic;

namespace SmplID.Derayah.Models.Gitlab
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string EmailID { get; set; }
        public string AccountStatus { get; set; }
    }
    public class Member
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string state { get; set; }
        public int access_level { get; set; }
        public string public_email { get; set; }
        public string entitlementId { get; set; }

    }
    public class PostMember
    {
        public string user_id { get; set; }
        public string access_level { get; set; }
    }
    public class MembersComparer : IEqualityComparer<Member>
    {
        public bool Equals(Member x, Member y)
        {
            return x.id == y.id;

        }
        public int GetHashCode(Member obj) => obj.id.GetHashCode();
    }
}
