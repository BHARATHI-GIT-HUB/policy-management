using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public long StateId { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Provider> Providers { get; set; } = new List<Provider>();

    public virtual State State { get; set; } = null!;
}
