using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class ApplyJob
{
    public int PostID { get; set; }

    public string EmployeeID { get; set; } = null!;

    public DateTime? AppliedDate { get; set; }

    public string? ApplicationStatus { get; set; }

    public virtual Employee? Employee { get; set; } = null!;

    public virtual JobPost? Post { get; set; } = null!;
}
