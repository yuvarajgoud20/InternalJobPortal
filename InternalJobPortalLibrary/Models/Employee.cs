using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternalJobPortalLibrary.Models;

public partial class Employee
{
    public string EmployeeID { get; set; } 

    public string? EmployeeName { get; set; }

    public string? EmailID { get; set; }
    [MaxLength(10, ErrorMessage = "Phone Numbers Must be 10 numbers")]
    public string? PhoneNo { get; set; }

    public int? TotalExperience { get; set; }

    public string? JobID { get; set; }

    public virtual ICollection<ApplyJob>? ApplyJobs { get; set; } = new List<ApplyJob>();

    public virtual ICollection<EmployeeSkill>? EmployeeSkills { get; set; } = new List<EmployeeSkill>();

    public virtual Job? Job { get; set; }
}
