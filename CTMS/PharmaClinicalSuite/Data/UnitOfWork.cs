using PharmaClinicalSuite.Models.Interfaces;

namespace PharmaClinicalSuite.Data
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly PharmaClinicalSuiteContext _context;

        public UnitOfWork(PharmaClinicalSuiteContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
