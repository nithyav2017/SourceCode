using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PatientInfo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PationtInfoDBConnection") ?? throw new InvalidOperationException("Connection string 'PationtInfoDBConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<LocalDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = true; options.User.RequireUniqueEmail = true; })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); 
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
