namespace Domain.Components.Abstractions
{
    public interface ICommitPackage
    {

    }

    public interface ICommitPackage<TAggregate> : ICommitPackage
        where TAggregate : IAggregate<TAggregate>
    {
        public TAggregate Aggregate { get; init; }
        public IEnumerable<IEvent<TAggregate>> Events { get; init; }

        public async Task Commit()
            => await Aggregate.Apply(Events.ToArray());
    }
}
