namespace PharmaClinicalSuite.Models.Interfaces
{
    public interface IDomainEvent
    {
        DateTime OccuredOn { get; }
    }
}
