using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class PlanSubtype
{
    public int Id { get; set; }

    public string Subtype { get; set; } = null!;

    public int TypeId { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    public virtual PlanType Type { get; set; } = null!;
}
