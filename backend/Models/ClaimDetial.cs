using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class ClaimDetial
{
    public int ClaimId { get; set; }

    public string Proof { get; set; } = null!;

    public string Reason { get; set; } = null!;

    public string Incident { get; set; } = null!;
}
