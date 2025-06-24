using MediatR;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PharmaClinicalSuite.Application.Events.ScheduledVisit;
using PharmaClinicalSuite.Data;
using PharmaClinicalSuite.Domain.Interfaces;
using PharmaClinicalSuite.Models.Interfaces;
using PharmaClinicalSuite.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<PharmaClinicalSuiteContext>(option => 
       //     option.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContextFactory<PharmaClinicalSuiteContext>(option =>
            option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();

builder.Services.AddMediatR(typeof(ScheduleVisitCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(ScheduleVisitEventHandler).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ClinicalTrial}/{action=Index}/{id?}");

app.Run();
