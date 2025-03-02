using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class JobSkill
{
    public string JobID { get; set; } = null!;

    public string SkillID { get; set; } = null!;

    public int? Experience { get; set; }

    public virtual Job Job { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
