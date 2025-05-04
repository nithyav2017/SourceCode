using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<LocalDbContext>(options =>
       options.UseSqlite(builder.Configuration.GetConnectionString("LocalDbConnection")));

var isOfflineMode = builder.Configuration["EnvironmentVariables:USE_SQLITE"] == "true";

builder.Services.AddSingleton<IServiceScopeFactory>(sp => sp.GetRequiredService<IServiceScopeFactory>());


builder.Services.AddScoped<IPatientService, PatientService>();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHostedService<BackgroundSyncService>();
builder.Services.AddScoped<ISyncService, SyncService>();
 
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure cookie policy
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services.AddDbContext<LocalDbContext>(options =>
{
    var isOfflineMode = Environment.GetEnvironmentVariable("USE_SQLITE") == "true";

    if (isOfflineMode)
        options.UseSqlite(builder.Configuration.GetConnectionString("LocalDbConnection"));
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteDbConnection"));
});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

//app.MapStaticAssets();
app.MapRazorPages();
 //  .WithStaticAssets();

app.Run();
    