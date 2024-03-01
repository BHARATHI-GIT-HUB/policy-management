using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Claim
{
    public int Id { get; set; }

    public int PolicyId { get; set; }

    public int StatusId { get; set; }

    public DateOnly Date { get; set; }

    public long Amount { get; set; }

    public virtual ClaimDetial IdNavigation { get; set; } = null!;

    public virtual PolicyEnrollment Policy { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
