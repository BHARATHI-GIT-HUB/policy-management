using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
