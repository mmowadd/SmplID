using System;
using System.Collections.Generic;

namespace SmplID.Derayah.Models
{
    public class Entitlement
    {
        public string EntitlementID { get; set; }
        public string EntitlementName { get; set; }
        public string Description { get; set; }
        public string ResourceType { get; set; }

        public int ExtractId() => Convert.ToInt32(EntitlementID.Split('-')[0]);
    }
    public class EntitlementComparer : IEqualityComparer<Entitlement>
    {
        public bool Equals(Entitlement x, Entitlement y)
        {
            return x.ExtractId() == y.ExtractId();

        }
        public int GetHashCode(Entitlement obj) => obj.ExtractId().GetHashCode();

    }
}
