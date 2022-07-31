namespace Domain.Components.Abstractions
{
    public interface IOperation : IServiceResult
    {
        public IAggregate Aggregate { get; }
        public ICommandResult CommandResult { get; }

        IEnumerable<IServiceResult> IServiceResult.Operations => new[] { this };
    }

    public interface IOperation<TAggregate> : IOperation
        where TAggregate : IAggregate
    {
        public new TAggregate Aggregate { get; }
        IAggregate IOperation.Aggregate => Aggregate;

        public new ICommandResult<TAggregate> CommandResult { get; }
        ICommandResult IOperation.CommandResult => CommandResult;
    }

    public interface IOperation<TAggregate, TResult> : IOperation<TAggregate>
        where TAggregate : IAggregate
        where TResult : ICommandResult<TAggregate>
    {
        public new TResult CommandResult { get; }
        ICommandResult<TAggregate> IOperation<TAggregate>.CommandResult => this.CommandResult;

        Task Evaluate();
    }
}
