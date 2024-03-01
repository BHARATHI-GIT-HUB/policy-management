using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepositryAssignement.Models;

public partial class InsuranceDbContext : DbContext
{
    public InsuranceDbContext()
    {
    }

    public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddOn> AddOns { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<ClaimDetial> ClaimDetials { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientKyc> ClientKycs { get; set; }

    public virtual DbSet<Incentive> Incentives { get; set; }

    public virtual DbSet<PaymentHistory> PaymentHistories { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<PlanSubtype> PlanSubtypes { get; set; }

    public virtual DbSet<PlanType> PlanTypes { get; set; }

    public virtual DbSet<PolicyEnrollment> PolicyEnrollments { get; set; }

    public virtual DbSet<PolicyHistory> PolicyHistories { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=BHARATHCOMPUTER;Database=insuranceDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddOn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Add_On__3213E83FEF9C740D");

            entity.ToTable("Add_On");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Coverage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("coverage");
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Agent__3213E83F0E24887B");

            entity.ToTable("Agent");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AadharNo).HasColumnName("aadhar_no");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mobile_no");
            entity.Property(e => e.PanNo).HasColumnName("pan_no");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.City).WithMany(p => p.Agents)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_city_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Agents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("agent_user_id_foreign");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__City__3213E83F3535B5D6");

            entity.ToTable("City");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StateId).HasColumnName("state_id");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("city_state_id_foreign");
        });

        modelBuilder.Entity<ClaimDetial>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claim_De__F9CC0896BAC7B15E");

            entity.ToTable("Claim_Detial");

            entity.Property(e => e.ClaimId).HasColumnName("claim_id");
            entity.Property(e => e.Incident)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("incident");
            entity.Property(e => e.Proof)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("proof");
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reason");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Client__3213E83FB6B9E65A");

            entity.ToTable("Client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.MailId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mail_id");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mobile_no");
            entity.Property(e => e.MotherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mother_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.City).WithMany(p => p.Clients)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_city_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("client_user_id_foreign");
        });

        modelBuilder.Entity<ClientKyc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Client_KYC");

            entity.Property(e => e.BankAccount)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bank_account");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ProofDob).HasColumnName("proof_dob");
            entity.Property(e => e.ProofIdentity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("proof_identity");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("client_kyc_client_id_foreign");
        });

        modelBuilder.Entity<Incentive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Incentiv__3213E83FE343C7B0");

            entity.ToTable("Incentive");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.AgentPerformance).HasColumnName("agent_performance");
            entity.Property(e => e.IncentiveAmount).HasColumnName("incentive_amount");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");

            entity.HasOne(d => d.Agent).WithMany(p => p.Incentives)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("incentive_agent_id_foreign");
        });

        modelBuilder.Entity<PaymentHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Payment_History");

            entity.Property(e => e.AmountPaid).HasColumnName("amount_paid");
            entity.Property(e => e.DateOfPayment).HasColumnName("date_of_payment");
            entity.Property(e => e.FineAmount).HasColumnName("fine_amount");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PolicyHistoryId).HasColumnName("policy_history_id");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plan__3213E83FA21E9FD1");

            entity.ToTable("Plan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddonId).HasColumnName("addon_id");
            entity.Property(e => e.CommissionPercentage).HasColumnName("commission_percentage");
            entity.Property(e => e.Deductibles).HasColumnName("deductibles");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.GeneralEliglibity)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("general_eliglibity");
            entity.Property(e => e.LaunchDate).HasColumnName("launch_date");
            entity.Property(e => e.MaxAgeEligiblity).HasColumnName("max_Age_eligiblity");
            entity.Property(e => e.MaxCoverageAmount).HasColumnName("max_Coverage_amount");
            entity.Property(e => e.MinIncomeEligiblity).HasColumnName("min_Income_eligiblity");
            entity.Property(e => e.PlanName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("plan_name");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.SubtypeId).HasColumnName("subtype_id");

            entity.HasOne(d => d.Addon).WithMany(p => p.Plans)
                .HasForeignKey(d => d.AddonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_add_on_id_foreign");

            entity.HasOne(d => d.Provider).WithMany(p => p.Plans)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_provider_id_foreign");

            entity.HasOne(d => d.Subtype).WithMany(p => p.Plans)
                .HasForeignKey(d => d.SubtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_subtype_id_foreign");
        });

        modelBuilder.Entity<PlanSubtype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plan_Sub__3213E83F98F75407");

            entity.ToTable("Plan_Subtype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Subtype)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subtype");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Type).WithMany(p => p.PlanSubtypes)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("plan_subtype_type_id_foreign");
        });

        modelBuilder.Entity<PlanType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Plan_Typ__3213E83FBDE4F7ED");

            entity.ToTable("Plan_Type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<PolicyEnrollment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Policy_E__3213E83FBD20CFE7");

            entity.ToTable("Policy_Enrollment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.CancelledOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cancelled_on");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CommisionAmount).HasColumnName("commision_amount");
            entity.Property(e => e.CoverageAmount).HasColumnName("coverage_amount");
            entity.Property(e => e.EnrolledOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("enrolled_on");
            entity.Property(e => e.ExpiredOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("expired_on");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("frequency");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Premium).HasColumnName("premium");
            entity.Property(e => e.TimePeriod).HasColumnName("time_period");

            entity.HasOne(d => d.Agent).WithMany(p => p.PolicyEnrollments)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_enrollment_agent_id_foreign");

            entity.HasOne(d => d.Client).WithMany(p => p.PolicyEnrollments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_enrollment_cilent_id_foreign");

            entity.HasOne(d => d.Plan).WithMany(p => p.PolicyEnrollments)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_enrollment_plan_id_foreign");
        });

        modelBuilder.Entity<PolicyHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Policy_H__3213E83FC30B6189");

            entity.ToTable("Policy_History");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AgentId).HasColumnName("agent_id");
            entity.Property(e => e.CancelledOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("cancelled_on");
            entity.Property(e => e.CilentId).HasColumnName("cilent_id");
            entity.Property(e => e.CommisionAmount).HasColumnName("commision_amount");
            entity.Property(e => e.CoverageAmount).HasColumnName("coverage_amount");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.EnrolledOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("enrolled_on");
            entity.Property(e => e.ExpiredOn)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("expired_on");
            entity.Property(e => e.Frequency).HasColumnName("frequency");
            entity.Property(e => e.PlanId).HasColumnName("plan_id");
            entity.Property(e => e.Premium).HasColumnName("premium");

            entity.HasOne(d => d.Agent).WithMany(p => p.PolicyHistories)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_History_agent_id_foreign");

            entity.HasOne(d => d.Cilent).WithMany(p => p.PolicyHistories)
                .HasForeignKey(d => d.CilentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_History_client_id_foreign");

            entity.HasOne(d => d.Plan).WithMany(p => p.PolicyHistories)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("policy_History_plan_id_foreign");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Provider__3213E83F500BED07");

            entity.ToTable("Provider");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_name");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LaunchDate).HasColumnName("launch_date");
            entity.Property(e => e.MailId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mail_ID");
            entity.Property(e => e.MoblieNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("moblie_no");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("phone_no");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.Testimonials)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("testimonials");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(NULL)")
                .HasColumnName("user_id");

            entity.HasOne(d => d.City).WithMany(p => p.Providers)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("provider_city id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Providers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("provider_user_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3213E83FF60EFA96");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__State__3213E83FE94C5534");

            entity.ToTable("State");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3213E83F9EEF8E9F");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83FCCED66E8");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
