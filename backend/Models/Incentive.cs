using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Incentive
{
    public int Id { get; set; }

    public int AgentId { get; set; }

    public int ProviderId { get; set; }

    public long IncentiveAmount { get; set; }

    public long AgentPerformance { get; set; }

    public virtual Agent Agent { get; set; } = null!;
}
