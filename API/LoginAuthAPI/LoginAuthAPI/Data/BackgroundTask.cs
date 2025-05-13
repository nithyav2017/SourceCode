
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class BackgroundTask : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundTask(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AuthDBContext>();

                    // Example: Fetch or process data
                    var users = await context.Users.ToListAsync();
                    Console.WriteLine($"Processed {users.Count} users at {DateTime.Now}");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Adjust delay as necessary
            }
        }
    }
}
