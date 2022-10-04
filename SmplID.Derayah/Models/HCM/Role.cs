using System.Collections.Generic;

namespace SmplID.Derayah.Models.HCM
{
    public class Role
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public List<HCMRoleMember> members { get; set; }
    }
    public class HCMRoleMember
    {
        public string value { get; set; }
    }
}
