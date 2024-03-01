using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Agent
{
    public int Id { get; set; }

    public DateOnly Dob { get; set; }

    public string Street { get; set; } = null!;

    public int CityId { get; set; }

    public string MobileNo { get; set; } = null!;

    public string Qualification { get; set; } = null!;

    public long AadharNo { get; set; }

    public long PanNo { get; set; }

    public int? UserId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Incentive> Incentives { get; set; } = new List<Incentive>();

    public virtual ICollection<PolicyEnrollment> PolicyEnrollments { get; set; } = new List<PolicyEnrollment>();

    public virtual ICollection<PolicyHistory> PolicyHistories { get; set; } = new List<PolicyHistory>();

    public virtual User? User { get; set; }
}
