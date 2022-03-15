using Domain.Components.Extensions;

namespace Domain.Components.Abstractions
{
    public interface ICommitPackage
    {
        public IAggregate Aggregate { get; }
        public IList<IEvent> Events { get; }

        public Task Apply() => Aggregate.Apply(Events.ToArray());
    }

    public interface ICommitPackage<TAggregate> : ICommitPackage
        where TAggregate : IAggregate
    {
        IAggregate ICommitPackage.Aggregate => Aggregate;
        IList<IEvent> ICommitPackage.Events => Events.Cast<IEvent>().ToList();

        public new IAggregate<TAggregate> Aggregate { get; init; }
        public new IList<IEvent<TAggregate>> Events { get; init; }

        public async Task Commit()
            => await Aggregate.Apply(Events.ToArray());
    }
}
