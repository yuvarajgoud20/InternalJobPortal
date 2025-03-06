using System.ComponentModel.DataAnnotations;

namespace InternalJobPortalMVC.Models
{
    public class Skill
    {
        [RegularExpression(@"\w{6}", ErrorMessage = "Skill ID Must be 6 chars")]
        public string SkillID { get; set; }
        [MaxLength(20, ErrorMessage = "Skill Name should not exceed 20 characters")]

        public string? SkillName { get; set; }

        public string? SkillLevel { get; set; }
    }
}
