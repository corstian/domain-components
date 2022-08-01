using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class Operation<TAggregate> : IOperation<TAggregate>
        where TAggregate : class, IAggregate<TAggregate>
    {
        private readonly TAggregate _aggregate;
        private readonly ICommand<TAggregate, ICommandResult<TAggregate>> _command;

        public Operation(TAggregate aggregate, ICommand<TAggregate, ICommandResult<TAggregate>> command)
        {
            _aggregate = aggregate;
            _command = command;
        }

        public TAggregate Aggregate { get; internal set; }
        public ICommandResult<TAggregate> Result { get; private set; }

        ICommand<TAggregate> IOperation<TAggregate>.Command => _command;

        async Task IOperation.Evaluate()
        {
            var result = await _aggregate.Evaluate(_command);

            if (result.IsSuccess)
            {
                Aggregate = _aggregate;
                Result = result.Value;
            }
        }
    }

    public class Operation<TAggregate, TResult> : IOperation<TAggregate, TResult>
        where TAggregate : class, IAggregate<TAggregate>
        where TResult : ICommandResult<TAggregate>
    {
        private readonly TAggregate _aggregate;
        private readonly ICommand<TAggregate, TResult> _command;

        public Operation(TAggregate aggregate, ICommand<TAggregate, TResult> command)
        {
            _aggregate = aggregate;
            _command = command;
        }

        public TAggregate Aggregate { get; internal set; }
        public TResult Result { get; private set; }

        ICommand<TAggregate, TResult> IOperation<TAggregate, TResult>.Command => _command;

        async Task IOperation.Evaluate()
        {
            var result = await _aggregate.Evaluate(_command);

            if (result.IsSuccess)
            {
                Aggregate = _aggregate;
                Result = result.Value;
            }
        }
    }
}
