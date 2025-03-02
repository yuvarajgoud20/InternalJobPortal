using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class Skill
{
    public string SkillID { get; set; } = null!;

    public string? SkillName { get; set; }

    public string? SkillLevel { get; set; }

    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual ICollection<JobSkill> JobSkills { get; set; } = new List<JobSkill>();
}
