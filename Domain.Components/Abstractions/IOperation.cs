namespace Domain.Components.Abstractions
{
    internal interface IOperation : IServiceResult
    {
        // Public to provide results
        IAggregate Aggregate { get; }
        ICommandResult Result { get; }

        IEnumerable<IServiceResult> IServiceResult.Operations => new[] { this };

        ICommand Command { get; }

        internal Task Evaluate();
    }

    internal interface IOperation<TAggregate> : IOperation
        where TAggregate : class, IAggregate
    {
        public new TAggregate Aggregate { get; }
        IAggregate IOperation.Aggregate => Aggregate;

        public new ICommandResult<TAggregate> Result { get; }
        ICommandResult IOperation.Result => Result;

        internal new ICommand<TAggregate> Command { get; }
        ICommand IOperation.Command => this.Command;
    }

    internal interface IOperation<TAggregate, TResult> : IOperation<TAggregate>
        where TAggregate : class, IAggregate
        where TResult : ICommandResult<TAggregate>
    {
        public new TResult Result { get; }
        ICommandResult<TAggregate> IOperation<TAggregate>.Result => this.Result;

        internal new ICommand<TAggregate, TResult> Command { get; }
        ICommand<TAggregate> IOperation<TAggregate>.Command => this.Command;
    }
}
