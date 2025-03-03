using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class Employee
{
    public string EmployeeID { get; set; } = null!;

    public string? EmployeeName { get; set; }

    public string? EmailID { get; set; }

    public string? PhoneNo { get; set; }

    public int? TotalExperience { get; set; }

    public string? JobID { get; set; }

    public virtual ICollection<ApplyJob>? ApplyJobs { get; set; } = new List<ApplyJob>();

    public virtual ICollection<EmployeeSkill>? EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual Job? Job { get; set; }
}
