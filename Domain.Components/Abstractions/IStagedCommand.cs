namespace Domain.Components.Abstractions
{
    public interface IStagedCommand : IServiceResult
    {
        public IAggregate Aggregate { get; }
        public ICommandResult CommandResult { get; }

        IEnumerable<IServiceResult> IServiceResult.Operations => new[] { this };
    }

    public interface IStagedCommand<TAggregate> : IStagedCommand
        where TAggregate : IAggregate
    {
        public new TAggregate Aggregate { get; }
        IAggregate IStagedCommand.Aggregate => Aggregate;

        public new ICommandResult<TAggregate> CommandResult { get; }
        ICommandResult IStagedCommand.CommandResult => CommandResult;
    }

    public interface IStagedCommand<TAggregate, TResult> : IStagedCommand<TAggregate>
        where TAggregate : IAggregate
        where TResult : ICommandResult<TAggregate>
    {
        public new TResult CommandResult { get; }
        ICommandResult<TAggregate> IStagedCommand<TAggregate>.CommandResult => this.CommandResult;

        Task Evaluate();
    }
}
