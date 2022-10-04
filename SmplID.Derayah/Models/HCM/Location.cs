namespace SmplID.Derayah.Models.HCM
{
    public class Location
    {
        public ulong LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationName { get; set; }
        public string AddressLine1 { get; set; }
        public string Country { get; set; }
        public string TownOrCity { get; set; }
    }
}
