namespace Domain.Components.Abstractions
{
    public interface ICommitPackage
    {
        public IAggregate Aggregate { get; }
        public IList<IEvent> Events { get; }
    }

    public interface ICommitPackage<TAggregate> : ICommitPackage
        where TAggregate : IAggregate<TAggregate>
    {
        IAggregate ICommitPackage.Aggregate => Aggregate;
        IList<IEvent> ICommitPackage.Events => Events.Cast<IEvent>().ToList();

        public new TAggregate Aggregate { get; init; }
        public new IList<IEvent<TAggregate>> Events { get; init; }

        public async Task Commit()
            => await Aggregate.Apply(Events.ToArray());
    }
}
