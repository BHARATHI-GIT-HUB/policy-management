using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class PolicyHistory
{
    public int Id { get; set; }

    public int PlanId { get; set; }

    public int AgentId { get; set; }

    public int CilentId { get; set; }

    public long CoverageAmount { get; set; }

    public long Frequency { get; set; }

    public long Premium { get; set; }

    public long CommisionAmount { get; set; }

    public long Duration { get; set; }

    public DateOnly? EnrolledOn { get; set; }

    public DateOnly? CancelledOn { get; set; }

    public DateOnly? ExpiredOn { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public virtual Client Cilent { get; set; } = null!;

    public virtual Plan Plan { get; set; } = null!;
}
