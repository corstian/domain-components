using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class CommitPackage<TAggregate> : ICommitPackage<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        public CommitPackage() { }
        public CommitPackage(IAggregate<TAggregate> aggregate, IEnumerable<IEvent<TAggregate>> events)
        {
            Aggregate = aggregate;
            Events = events.ToList();
        }

        public IAggregate<TAggregate> Aggregate { get; init; }
        public IList<IEvent<TAggregate>> Events { get; init; } = new List<IEvent<TAggregate>>();
    }
}
