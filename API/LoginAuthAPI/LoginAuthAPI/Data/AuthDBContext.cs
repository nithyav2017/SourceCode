using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class AuthDBContext:DbContext
    {
        private readonly TaskService _taskService;
        public AuthDBContext(DbContextOptions<AuthDBContext> options, TaskService taskService) : base(options)
        { 
            this._taskService = taskService;
        }
      
        public DbSet<Person> Users { get; set; }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=AdventureWorks2019;Integrated Security=true;TrustServerCertificate=True;Max Pool Size=100;");
        //}

        private static readonly Func<AuthDBContext, int, IEnumerable<Person>> GetUserCompiledQuery =
    EF.CompileQuery((AuthDBContext context, int id) =>
        context.Users.Where(e => e.BusinessEntityID == id)
    );
        public async Task<Person> FetchEntities(AuthDBContext context, int id)
        {
            var user = await _taskService.FetchUsersAsync(id);
            return user;
               
                //var user = await context.Users.FindAsync(id);
                //return user; 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .ToTable("Person", "Person").HasKey(p=>p.BusinessEntityID); // Ensure the table name is set
        }


    }
}

