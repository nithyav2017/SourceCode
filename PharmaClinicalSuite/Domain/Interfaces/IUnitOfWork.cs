namespace PharmaClinicalSuite.Models.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync(CancellationToken cancellationToken=default);
    }
}
