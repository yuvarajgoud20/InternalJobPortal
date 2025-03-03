using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class Job
{
    public string JobID { get; set; } = null!;

    public string? JobTitle { get; set; }

    public string? JobDescription { get; set; }

    public decimal? Salary { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; } = new List<Employee>();

    public virtual ICollection<JobPost>? JobPosts { get; set; } = new List<JobPost>();

    public virtual ICollection<JobSkill>? JobSkills { get; set; } = new List<JobSkill>();
}
