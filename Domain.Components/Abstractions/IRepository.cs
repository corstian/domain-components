namespace Domain.Components.Abstractions
{
    public interface IRepository<T>
        where T : IAggregate
    {
        T ById(Guid id);
    }
}
