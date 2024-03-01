using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class State
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
