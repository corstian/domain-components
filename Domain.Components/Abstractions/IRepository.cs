namespace Domain.Components.Abstractions
{
    public interface IRepository<T>
        where T : IAggregate
    {
        Task<T> ById(Guid id);
    }
}
