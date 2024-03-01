using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Provider> Providers { get; set; } = new List<Provider>();

    public virtual Role Role { get; set; } = null!;
}
