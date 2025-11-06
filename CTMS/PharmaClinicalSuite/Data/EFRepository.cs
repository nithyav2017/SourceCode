using Microsoft.EntityFrameworkCore;
using PharmaClinicalSuite.Models.Interfaces;

namespace PharmaClinicalSuite.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly PharmaClinicalSuiteContext _context;

        public EFRepository(PharmaClinicalSuiteContext context)
        {
            _context = context;
        }


        void IRepository<T>.Add(T entity)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<T>> IRepository<T>.GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        async Task<T> IRepository<T>.GetByIdAsync(int Id)
            => await _context.Set<T>().FindAsync(Id);

        void IRepository<T>.Remove(T entity)
             => _context.Set<T>().Remove(entity);

        void IRepository<T>.Update(T entity)
            => _context.Update(entity);
    }
}
