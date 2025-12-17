using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebApplication.Data;
using WebApplication.Interfaces;
using WebApplication.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApplications.Interfaces;
using WebApplications.Services;


var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
//builder.Services.AddResponseCaching(); //Caching only HTTP responses

builder.Services.AddDbContext<AdventureworksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorks")));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "SampleInstance";
});

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder =>
    {
        builder.Expire(TimeSpan.FromSeconds(600));
    });
});

var redisConnection = builder.Configuration.GetConnectionString("Redis");
builder.Services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(redisConnection!));

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<WebApplications.Services.Redis.RedisBackgroundService.CacheInvalidationSubscriber>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
}).AddEntityFrameworkStores<AdventureworksContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseResponseCaching();
app.UseOutputCache();

app.MapControllers();

app.Run();
