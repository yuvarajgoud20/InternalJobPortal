namespace InternalJobPortalMVC.Models
{
    public class ApplyJob
    {
        public int PostID { get; set; }

        public string EmployeeID { get; set; } 

        public DateTime? AppliedDate { get; set; }

        public string? ApplicationStatus { get; set; }
    }
}
