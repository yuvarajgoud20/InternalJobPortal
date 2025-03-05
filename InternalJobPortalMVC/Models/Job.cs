namespace InternalJobPortalMVC.Models
{
    public class Job
    {
        public string JobID { get; set; }

        public string? JobTitle { get; set; }

        public string? JobDescription { get; set; }

        public decimal? Salary { get; set; }
    }
}
