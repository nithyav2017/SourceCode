using ArthritisPatientPortal.Data;
 
namespace BackGroundSyncService
{
    public class SyncDataService : BackgroundService
    {
        private readonly ILogger<SyncDataService> _logger;
        private readonly IServiceProvider _serviceProvider;

       
        public SyncDataService(ILogger<SyncDataService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                using (var scope = _serviceProvider.CreateScope())
                {
                    var syncService = scope.ServiceProvider.GetRequiredService<SyncService>();
                    await syncService.SyncDataAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
