using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using BackGroundSyncService;
using Microsoft.EntityFrameworkCore;
try
{
    var builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer("Server=.;Database=PatientInfoDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"));
    builder.Services.AddDbContext<LocalDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("LocalDbConnection")));
    builder.Services.AddHostedService<SyncDataService>();
    builder.Services.AddScoped<ISyncService ,SyncService>(); 
    var host = builder.Build();
    host.Run();
}
catch(Exception ex)
{
    File.WriteAllText(@"C:\Users\djaya\Documents\Nithya\DotNetCore\ArthritisPatientPortal\BackGroundSyncService\error_log.txt", ex.ToString());
}
