using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class CommitPackageBuilder
    {
        internal virtual List<ICommand> CommandList { get; }
    }

    public class CommitPackageBuilder<TAggregate> : CommitPackageBuilder
        where TAggregate : IAggregate<TAggregate>
    {
        internal override List<ICommand> CommandList => Commands.Cast<ICommand>().ToList();
        public List<ICommand<TAggregate>> Commands { get; } = new();

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

        //public async Task<IResult<ICommitPackage<TAggregate>>> CreatePackage(TAggregate aggregate)
        //{
        //    var results = new List<IResult<IEnumerable<IEvent<TAggregate>>>>();

        //    foreach (var command in _commands)
        //        results.Add(await aggregate.Evaluate(command));

        //    if (results.Any(q => q.IsFailed))
        //        return new DomainResult<ICommitPackage<TAggregate>>()
        //            .WithReasons(results.SelectMany(q => q.Reasons));

        //    return DomainResult.Ok(
        //        new CommitPackage<TAggregate>(
        //            aggregate,
        //            results.SelectMany(q => q.Value)));
        //}
    }
}
