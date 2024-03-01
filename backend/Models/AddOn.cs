using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class AddOn
{
    public int Id { get; set; }

    public string? Coverage { get; set; }

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();
}
