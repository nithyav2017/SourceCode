using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebApplication.Data;
using WebApplication.Interfaces;
using WebApplication.Services;
using WebApplications.Services.Redis.BackgroundService;

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
builder.Services.AddMemoryCache();
builder.Services.AddHostedService<WebApplications.Services.Redis.RedisBackgroundService.CacheInvalidationSubscriber>();
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
