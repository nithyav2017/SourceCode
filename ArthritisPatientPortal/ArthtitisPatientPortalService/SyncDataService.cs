using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthtitisPatientPortalService
{
    public class SyncDataService : ISyncDataService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISyncService _syncService;
        private readonly LocalDbContext _localDbContext;
        private readonly ApplicationDbContext _applicationDbContext;

        public SyncDataService(IServiceScopeFactory scopeFactory, ISyncService syncService, LocalDbContext localDbContext, ApplicationDbContext applicationDbContext)
        {
            _scopeFactory = scopeFactory;
            _syncService = syncService;
            _localDbContext = localDbContext;
            _applicationDbContext = applicationDbContext;
        }
        public async Task SyncPatientDataAsync(IServiceScopeFactory scopeFactory)
        {
            //using (var scope = scopeFactory.CreateScope()) //create scoped instance of SyncService
            //{
            //    //var syncService = scope.ServiceProvider.GetRequiredService<SyncService>(); // retrieve an instance of SyncService
             
            //}
            await _syncService.SyncDataAsync();
        }
    }
}
