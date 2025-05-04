using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientInfo.Models;

namespace PatientInfo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<PatientDetails>().ToTable("dbo.Patients")
                .HasOne(p => p.User)
                .WithOne(u => u.PatientDetails)
                .HasForeignKey<PatientDetails>(p => p.UserId);
        }
    }
}
