using Microsoft.EntityFrameworkCore;
using PharmaClinicalSuite.Models.Interfaces;
using System.Linq.Expressions;

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

        //method for lookup by any field

        /// <summary>
        /// predicate is a filter condiation passed as a lambda expression that defines which records to retrieve from the database
        /// it takes an object of type  T and returns a bool , it is used in the where condition like ParticipantId = 5 , 
        /// Expression<Func<T, bool>> predicate can be used to compare any field of a type, ex: visit.visitdate , User.Role="admin"
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        async Task<List<T?>> IRepository<T>.GetByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            if (_context == null)
                throw new ArgumentNullException("The context object is not initialized");

            return await _context.Set<T>().Where(predicate).ToListAsync() as List<T?>;
        }
    }
}
