using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthtitisPatientPortalService
{
    public interface ISyncDataService
    {
         Task SyncPatientDataAsync(IServiceScopeFactory scopeFactory);
    }
}
