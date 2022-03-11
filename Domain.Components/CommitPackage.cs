using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class CommitPackage
    {
        internal CommitPackage() { }

        public IAggregate Aggregate { get; init; }
        public IEnumerable<IEvent> Events { get; init; }
    }

    public class CommitPackage<TAggregate> : CommitPackage, ICommitPackage<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        public CommitPackage() { }
        public CommitPackage(TAggregate aggregate, IEnumerable<IEvent<TAggregate>> events)
        {
            Aggregate = aggregate;
            Events = events;
        }

        public new TAggregate Aggregate { get; init; }
        public new IEnumerable<IEvent<TAggregate>> Events { get; init; }
    }
}
