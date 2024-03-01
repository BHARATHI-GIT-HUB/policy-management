using System;
using System.Collections.Generic;

namespace RepositryAssignement.Models;

public partial class Client
{
    public int Id { get; set; }

    public DateOnly Dob { get; set; }

    public string MobileNo { get; set; } = null!;

    public string MailId { get; set; } = null!;

    public string FatherName { get; set; } = null!;

    public string MotherName { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string Street { get; set; } = null!;

    public int CityId { get; set; }

    public int? UserId { get; set; }

    public virtual City City { get; set; } = null!;

    public virtual ICollection<PolicyEnrollment> PolicyEnrollments { get; set; } = new List<PolicyEnrollment>();

    public virtual ICollection<PolicyHistory> PolicyHistories { get; set; } = new List<PolicyHistory>();

    public virtual User? User { get; set; }
}
