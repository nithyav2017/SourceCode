using ArthritisPatientPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ArthritisPatientPortal.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<CopayCard> CopayCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CopayCard>()
                .HasOne(c => c.patient)
                .WithMany()
                .HasForeignKey(c => c.PatientId);
        }
    }
}
