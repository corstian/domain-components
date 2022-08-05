namespace Domain.Components.Abstractions
{
    public interface IAggregateProvider
    {
        public IAggregate<TAggregate> Get<TAggregate>(Guid id)
            where TAggregate : class, IAggregate<TAggregate>;
    }
}
