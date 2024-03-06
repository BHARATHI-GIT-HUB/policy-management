using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RepositryAssignement.Models;

public partial class Provider
{
    public int Id { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string MoblieNo { get; set; } = null!;

    public string MailId { get; set; } = null!;

    public int CityId { get; set; }

    public string Street { get; set; } = null!;

    public DateOnly LaunchDate { get; set; }

    public string? Testimonials { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public int? UserId { get; set; }

    public string? CompanyName { get; set; }

    [JsonIgnore]
    public virtual City? City { get; set; } = null!;

    public virtual ICollection<Plan> Plans { get; set; } = new List<Plan>();

    [JsonIgnore]
    public virtual User? User { get; set; }
}
