using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RepositryAssignement.Models;

public partial class Plan
{
    public int Id { get; set; }

    public int SubtypeId { get; set; }

    public int ProviderId { get; set; }

    public int AddonId { get; set; }

    public string PlanName { get; set; } = null!;

    public long Deductibles { get; set; }

    public long MaxCoverageAmount { get; set; }

    public DateOnly LaunchDate { get; set; }

    public string Description { get; set; } = null!;

    public long MaxAgeEligiblity { get; set; }

    public long MinIncomeEligiblity { get; set; }

    public string GeneralEliglibity { get; set; } = null!;

    public long CommissionPercentage { get; set; }
    [JsonIgnore]
    public virtual AddOn Addon { get; set; } = null!;

    public virtual ICollection<PolicyEnrollment> PolicyEnrollments { get; set; } = new List<PolicyEnrollment>();

    public virtual ICollection<PolicyHistory> PolicyHistories { get; set; } = new List<PolicyHistory>();
   
    [JsonIgnore]
    public virtual Provider Provider { get; set; } = null!;
    
    [JsonIgnore]
    public virtual PlanSubtype Subtype { get; set; } = null!;
}
