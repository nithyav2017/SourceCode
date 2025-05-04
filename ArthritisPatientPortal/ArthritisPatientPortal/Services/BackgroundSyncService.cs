
using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Models;

namespace ArthritisPatientPortal.Services
{
    public class BackgroundSyncService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ISyncService _syncService;

        public BackgroundSyncService(IServiceProvider serviceProvider, ISyncService syncService)
        {
            _serviceProvider = serviceProvider;
            _syncService = syncService;

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var syncService = scope.ServiceProvider.GetRequiredService<SyncService>();
                    await syncService.SyncDataAsync();
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // Sync every 5 minutes
            }
        }

        //public async Task ResolveConflicts(Patient localPatient, Patient remotePatient)
        //{
        //    if (localPatient.LastUpdated > remotePatient.LastUpdated)
        //    {
        //        remotePatient.Name = localPatient.Name;  // Use latest version
        //        remotePatient.LastUpdated = DateTime.UtcNow;
        //    }
        //    else
        //    {
        //        localPatient.Name = remotePatient.Name;  // Sync older data
        //        localPatient.LastUpdated = DateTime.UtcNow;
        //    }
        //}
    }
}
