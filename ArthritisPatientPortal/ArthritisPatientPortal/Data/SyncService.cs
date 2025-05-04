using ArthritisPatientPortal.Interface;

namespace ArthritisPatientPortal.Data
{
    public class SyncService:ISyncService
    {
        private readonly LocalDbContext _localDb;
        private readonly ApplicationDbContext _remoteDb;

        public SyncService(LocalDbContext localDb, ApplicationDbContext remoteDb)
        {
            _localDb = localDb;
            _remoteDb = remoteDb;
        }

        public async Task SyncDataAsync()
        {
            var unsyncedPatients = _localDb.Patients.Where(p => !p.IsSynced).ToList();

            foreach (var patient in unsyncedPatients)
            {
                _remoteDb.Patients.Add(patient);
                patient.IsSynced = true;  // Mark as synced
            }

            await _remoteDb.SaveChangesAsync();
            await _localDb.SaveChangesAsync();
        }
    }
}
