namespace PharmaClinicalSuite.Models.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

    }
}
