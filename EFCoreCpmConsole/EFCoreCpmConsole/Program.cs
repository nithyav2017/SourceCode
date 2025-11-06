// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

var services = new ServiceCollection();

services.AddDbContext<EFCoreCpmConsole.MyContext>(options =>
    options.UseSqlServer("Server=localhost,1433;Database=POC;User Id=sa;Password=Learning@12;TrustServerCertificate=True;"));

var sp = services.BuildServiceProvider();

using var context = sp.GetRequiredService<EFCoreCpmConsole.MyContext>();

Console.WriteLine("Connected to database successfully.");