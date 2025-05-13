using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
 
builder.Services.AddDbContext<AuthDBContext>( options =>
    options.UseSqlServer("Data Source=.;Initial Catalog=AdventureWorks2019;Integrated Security=true;TrustServerCertificate=True;Max Pool Size=100;"));
builder.Services.AddScoped<TaskService>();
var app = builder.Build();
//var serviceProvider = app.Services;
//using(var scope = serviceProvider.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<AuthDBContext>();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
