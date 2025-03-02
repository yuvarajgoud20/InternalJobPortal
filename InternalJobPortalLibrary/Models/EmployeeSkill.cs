using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class EmployeeSkill
{
    public string EmployeeID { get; set; } = null!;

    public string SkillID { get; set; } = null!;

    public int? SkillExperience { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
