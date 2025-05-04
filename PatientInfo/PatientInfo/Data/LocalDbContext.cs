using Microsoft.EntityFrameworkCore;
using PatientInfo.Models;

namespace PatientInfo.Data
{
    public class LocalDbContext:DbContext
    {
        public DbSet<PatientDetails> PatientDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=localdata.db");
        }
    }
}
