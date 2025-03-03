using System;
using System.Collections.Generic;

namespace InternalJobPortalLibrary.Models;

public partial class JobPost
{
    public int PostID { get; set; }

    public string? JobID { get; set; }

    public DateTime? DateOfPosting { get; set; }

    public DateTime? LastDateToApply { get; set; }

    public int? NoOfVacancies { get; set; }

    public virtual ICollection<ApplyJob>? ApplyJobs { get; set; } = new List<ApplyJob>();

    public virtual Job? Job { get; set; }
}
