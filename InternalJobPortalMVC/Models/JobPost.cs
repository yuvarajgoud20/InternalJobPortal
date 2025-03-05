namespace InternalJobPortalMVC.Models
{
    public class JobPost
    {
        public int PostID { get; set; }

        public string? JobID { get; set; }

        public DateTime? DateOfPosting { get; set; }

        public DateTime? LastDateToApply { get; set; }

        public int? NoOfVacancies { get; set; }
    }
}
