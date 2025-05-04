using ArthritisPatientPortal.Models;
using Microsoft.EntityFrameworkCore;
  

namespace ArthritisPatientPortal.Data
{
    public class LocalDbContext: DbContext
    {    

        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\djaya\\Documents\\Nithya\\DotNetCore\\ArthritisPatientPortal\\ArthtitisPatientPortalService\\localdata.db");
        }
    }
}
