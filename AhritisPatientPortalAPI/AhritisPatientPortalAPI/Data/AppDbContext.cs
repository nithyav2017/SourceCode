using clsArthritisPatient;
using Microsoft.EntityFrameworkCore;

namespace AhritisPatientPortalAPI.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Patient> patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Patient>(entity =>
            {
                //entity.HasKey(p => p.Id);
                entity.Property(p => p.FirstName);
                entity.Property(p => p.LastName);
                entity.Property(p => p.Email);
                entity.Property(p => p.InsuranceType);
                entity.Property(p => p.DateOfBirth);
                entity.Property(p => p.HcpSpecialty);
                entity.Property(p => p.PinHash);
                entity.Property(p => p.Phone);
                entity.Property(p => p.ConsentToEmail);
                entity.Property(p => p.ConsentToText);              

            });
        }
    }
}
