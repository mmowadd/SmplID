using System.Collections.Generic;

namespace SmplID.Derayah.Models.HCM
{
    public class Response<T> where T : class
    {
        public List<T> items { get; set; }
    }
    public class ScimResponse<T> where T : class
    {
        public List<T> Resources { get; set; }
    }
}
