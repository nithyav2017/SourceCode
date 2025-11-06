using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCpmConsole
{
    public class MyContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
           var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=POC;User Id=sa;Password=Learning@12;TrustServerCertificate=True;");
            return new MyContext(optionsBuilder.Options);
        }
    }
}
