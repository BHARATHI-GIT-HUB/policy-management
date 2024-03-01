using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class PlanType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<PlanSubtype> PlanSubtypes { get; set; } = new List<PlanSubtype>();
}
