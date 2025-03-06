using System.ComponentModel.DataAnnotations;

namespace InternalJobPortalMVC.Models
{
    public class Employee
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "EmployeeId Must be 6 chars")]
        public string EmployeeID { get; set; }

        public string? EmployeeName { get; set; }

        public string? EmailID { get; set; }
        [MaxLength(10, ErrorMessage = "Phone Numbers Must be 10 numbers")]
        public string? PhoneNo { get; set; }

        public int? TotalExperience { get; set; }

        public string? JobID { get; set; }
    }
}
