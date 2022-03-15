using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class CommitPackageBuilder<TAggregate> : ICommitPackageBuilder<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        public IAggregate<TAggregate> Aggregate { get; init; }

        public IList<ICommand<TAggregate>> Commands { get; } = new List<ICommand<TAggregate>>();
        
        public CommitPackageBuilder<TAggregate> IncludeCommand(ICommand<TAggregate> command)
        {
            Commands.Add(command);
            return this;
        }

        public CommitPackageBuilder<TAggregate> IncludeCommand(ICommand<TAggregate, IEvent<TAggregate>> command)
        {
            Commands.Add(new GenericCommandWrapper<TAggregate, IEvent<TAggregate>>(command));
            return this;
        }

        public CommitPackageBuilder<TAggregate> IncludeCommand(ICommand<TAggregate, IEvent<TAggregate>, IEvent<TAggregate>> command)
        {
            Commands.Add(new GenericCommandWrapper<TAggregate, IEvent<TAggregate>, IEvent<TAggregate>>(command));
            return this;
        }
    }
}
