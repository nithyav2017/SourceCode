using System.Linq.Expressions;

namespace PharmaClinicalSuite.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<List<T?>> GetByConditionAsync(Expression<Func<T, bool>> predicate);

    }
}
