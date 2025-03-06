using System.ComponentModel.DataAnnotations;

namespace InternalJobPortalMVC.Models
{
    public class Job
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "Job ID Must be 6 chars")]
        public string JobID { get; set; }
        [MaxLength(20, ErrorMessage = "Title should not exceed 20 characters")]
        public string? JobTitle { get; set; }
        [MaxLength(300, ErrorMessage = "Description should not exceed 300 characters")]
        public string? JobDescription { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Salary must be greater than zero.")]
        public decimal? Salary { get; set; }
    }
}
