using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using ArthtitisPatientPortalService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.Common;
using System.Reflection;

 
var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddScoped<ISyncDataService, SyncDataService>();
        services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer("Server=.;Database=PatientInfoDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"));
        services.AddDbContext<LocalDbContext>(options =>
            options.UseSqlite("Data Source=C:\\Users\\djaya\\Documents\\Nithya\\DotNetCore\\ArthritisPatientPortal\\ArthtitisPatientPortalService\\localdata.db")); // SQLite database path
        services.AddScoped<ISyncService, SyncService>();
    })
    .Build();

host.Run();
