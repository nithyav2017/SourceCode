
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PharmaClinicalSuite;
using PharmaClinicalSuite.Domain.Models;
using static PharmaClinicalSuite.Domain.Models.Data_Collection;

namespace PharmaClinicalSuite.Data;

public partial class PharmaClinicalSuiteContext : DbContext
{
    public PharmaClinicalSuiteContext(DbContextOptions<PharmaClinicalSuiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdverseEvents> AdverseEvents { get; set; }

    public virtual DbSet<AuditTrail> AuditTrails { get; set; }

    public virtual DbSet<CaseReportform> CaseReportforms { get; set; }

    public virtual DbSet<DataCollectionFields> DataCollectionFields { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Investigator> Investigator { get; set; }

    public virtual DbSet<Participants> Participants { get; set; }

    public virtual DbSet<ParticipantFieldData> ParticipantFieldData { get; set; }

    public virtual DbSet<ParticipantFormEntries> ParticipantFormEntries { get; set; }

    public virtual DbSet<Sites> Sites { get; set; }

    public virtual DbSet<Trials> Trials { get; set; }

    public virtual DbSet<TrialInvestigator> TrialsInvestigators { get; set; }

    public virtual DbSet<TrialType> TrialTypes { get; set; }

    public virtual DbSet<WithdrawalReason> WithdrawalReasons { get; set; }

  public virtual DbSet<Visit> Visit { get; set; }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdverseEvents>(entity =>
        {
            entity.HasKey(e => e.AdverseEventId).HasName("PK__AdverseE__9AA4ACB0F54C4ED7");

            entity.HasOne(d => d.Participants).WithMany(p => p.AdverseEvents)
            .HasForeignKey(p => p.ParticipantId).IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdverseEv__Parti__4BAC3F29");

            entity.HasOne(d => d.Trials).WithMany(p => p.AdverseEvents)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AdverseEv__Trial__4CA06362");
        });

        modelBuilder.Entity<AuditTrail>(entity =>
        {
            entity.HasKey(e => e.AuditTrailId).HasName("PK__AuditTra__41B2DDB36F2CE413");

            entity.Property(e => e.Timestamp).HasDefaultValueSql("(getutcdate())");
        });

        modelBuilder.Entity<CaseReportform>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__CaseRepo__FB05B7BD5C1893E6");

            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Trials).WithMany(p => p.CaseReportForms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CaseRepor__Trial__5BE2A6F2");
        });

        modelBuilder.Entity<DataCollectionFields>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__DataColl__C8B6FF273546AC0B");

            entity.Property(e => e.IsRequired).HasDefaultValue(false);

            entity.HasOne(d => d.Forms).WithMany(p => p.DataCollectionFields)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DataColle__FormI__60A75C0F");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FBF7A96B3D");

            entity.HasOne(d => d.Participants).WithMany(p => p.Enrollments)
                 .HasForeignKey(d => d.ParticipantId).IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Parti__45F365D3");

            entity.HasOne(d => d.Trials).WithMany(p => p.Enrollments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Enrollmen__Trial__46E78A0C");

            entity.HasOne(d => d.WithdrawalReasons).WithMany(p => p.Enrollments).HasConstraintName("FK__Enrollmen__Withd__47DBAE45");
        });

        modelBuilder.Entity<Investigator>(entity =>
        {
            entity.HasKey(e => e.InvestigatorId).HasName("PK__Investig__A09546DB837CFE7F");
        });

        modelBuilder.Entity<Participants>(entity =>
        {

            entity.HasKey(e => e.ParticipantId).HasName("PK__Particip__7227997E3187744A");

            entity.Property(e => e.Gender).IsFixedLength();
        });

        modelBuilder.Entity<ParticipantFieldData>(entity =>
        {
            entity.HasKey(e => e.DataId).HasName("PK__Particip__9D05305D2A166DB6");

            entity.HasOne(d => d.ParticipantFormEntries).WithMany(p => p.ParticipantFieldData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Entry__68487DD7");

            entity.HasOne(d => d.DataCollectionFields).WithMany(p => p.ParticipantFieldData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__Field__693CA210");
        });

        modelBuilder.Entity<ParticipantFormEntries>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__Particip__F57BD2D7C4C40332");

            entity.Property(e => e.EntryDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CaseReportforms).WithMany(p => p.ParticipantFormEntries)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Participa__FormI__656C112C");

            /*entity.HasOne(d => d.ParticipantFieldData).WithMany(p => p.ParticipantFormEntries)
                 .OnDelete(DeleteBehavior.ClientSetNull)
                 .HasConstraintName("FK__Participa__Parti__6477ECF3");*/
        });

        modelBuilder.Entity<Sites>(entity =>
        {
            entity.HasKey(e => e.SiteId).HasName("PK__Sites__B9DCB903D6784133");
        });

        modelBuilder.Entity<Trials>(entity =>
        {
            entity.HasKey(e => e.TrialId).HasName("PK__Trials__EF1025A4C7A82768");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.TrialsType).WithMany(p => p.Trials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Trial_TrialType");

            entity.HasMany(d => d.Sites).WithMany(p => p.Trials)
                .UsingEntity<Dictionary<string, object>>(
                    "TrialSite",
                    r => r.HasOne<Sites>().WithMany()
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TrialSite__SiteI__5812160E"),
                    l => l.HasOne<Trials>().WithMany()
                        .HasForeignKey("TrialId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TrialSite__Trial__571DF1D5"),
                    j =>
                    {
                        j.HasKey("TrialId", "SiteId").HasName("PK__TrialSit__C48DEE34904186B5");
                        j.ToTable("TrialSites");
                        j.IndexerProperty<int>("TrialId").HasColumnName("TrialID");
                        j.IndexerProperty<int>("SiteId").HasColumnName("SiteID");
                    });
        });

        modelBuilder.Entity<TrialInvestigator>(entity =>
        {
            entity.HasKey(e => new { e.TrialId, e.InvestigatorId }).HasName("PK__TrialInv__451971C9ECD67464");

            entity.HasOne(d => d.Investigator).WithMany(p => p.TrialsInvestigators)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrialInve__Inves__52593CB8");

            entity.HasOne(d => d.Trials).WithMany(p => p.TrialsInvestigators)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TrialInve__Trial__5165187F");
        });
        modelBuilder.Entity<Visit>(e =>
        {
            e.HasKey(e => new { e.Id }).HasName("PK_Visit");
            e.HasOne(e => e.Participant).WithMany(e => e.Visits)
              .OnDelete(DeleteBehavior.ClientSetNull)
              .HasConstraintName("FK_Participant_Id");
        }); 
       
            

        modelBuilder.Entity<TrialType>(entity =>
        {
            entity.HasKey(e => e.TrialTypeId).HasName("PK__TrialTyp__59CAB3A78A1EBCDE");
        });

        modelBuilder.Entity<WithdrawalReason>(entity =>
        {
            entity.HasKey(e => e.WithdrawalReasonID).HasName("PK__Withdraw__BE8C1525ACA37278");



        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
