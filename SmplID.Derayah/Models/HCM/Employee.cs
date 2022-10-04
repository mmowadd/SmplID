using System.Collections.Generic;

namespace SmplID.Derayah.Models.HCM
{
    public class Employee
    {
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string PersonNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string WorkMobilePhoneNumber { get; set; }
        public string HomePhoneNumber { get; set; }
        public string WorkEmail { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string HireDate { get; set; }
        public string TerminationDate { get; set; }
        public string Gender { get; set; }
        public ulong PersonId { get; set; }
        public string UserName { get; set; }
        public string WorkerType { get; set; }
        public List<Assignment> assignments { get; set; }
    }

    public class Assignment
    {
        public string AssignmentName { get; set; }
        public ulong? LocationId { get; set; }
        public ulong? DepartmentId { get; set; }
        public string AssignmentStatus { get; set; }
        public string EffectiveStartDate { get; set; }
        public string EffectiveEndDate { get; set; }
        public ulong? PositionId { get; set; }
        public ulong? ManagerId { get; set; }
    }
}
