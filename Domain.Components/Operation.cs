using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class Operation<TAggregate> : IOperation<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        public Operation(TAggregate aggregate, ICommandResult<TAggregate> command)
        {
            Aggregate = aggregate;
            CommandResult = command;
        }

        public TAggregate Aggregate { get; }
        public ICommandResult<TAggregate> CommandResult { get; }
    }

    public class Operation<TAggregate, TCommand> : IOperation<TAggregate, TCommand>
        where TAggregate : IAggregate<TAggregate>
        where TCommand : ICommandResult<TAggregate>
    {
        private Func<TAggregate, TCommand> _commandEvaluation;

        public Operation(TAggregate aggregate, Func<TAggregate, TCommand> command)
        {
            Aggregate = aggregate;
            _commandEvaluation = command;
        }

        public async Task Evaluate()
        {
            CommandResult = _commandEvaluation(Aggregate);
        }

        public TAggregate Aggregate { get; init; }
        
        public TCommand CommandResult { get; private set; }
    }
}
