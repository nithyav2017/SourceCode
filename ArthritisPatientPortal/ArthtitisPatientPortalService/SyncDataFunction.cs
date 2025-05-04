using System;
using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace ArthtitisPatientPortalService
{
    public class SyncDataFunction
    {
        private readonly ILogger _logger;

        private readonly ISyncDataService _syncDataService;
        private readonly  IServiceScopeFactory _scopeFactory;
        
        public SyncDataFunction(ILoggerFactory loggerFactory , ISyncDataService syncDataService, IServiceScopeFactory scopeFactory )
        {
            _logger = loggerFactory.CreateLogger<SyncDataFunction>();
            _syncDataService = syncDataService;
            _scopeFactory = scopeFactory;
          
        }

        [Function("SyncData")]
        public async Task Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Sync Data: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }

             
              await _syncDataService.SyncPatientDataAsync(_scopeFactory);

            _logger.LogInformation("Data sync completed.");
        }
    }
}
