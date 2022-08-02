using Domain.Components.Abstractions;

namespace Domain.Components
{
    public sealed class Operation<TAggregate> : IOperation<TAggregate>
        where TAggregate : class, IAggregate<TAggregate>
    {
        private readonly TAggregate _aggregate;
        private readonly ICommand<TAggregate, ICommandResult<TAggregate>> _command;

        public Operation(TAggregate aggregate, ICommand<TAggregate, ICommandResult<TAggregate>> command)
        {
            _aggregate = aggregate;
            _command = command;
        }

        public TAggregate Aggregate => _aggregate;
        public ICommandResult<TAggregate> Result { get; private set; }

        ICommand<TAggregate> IOperation<TAggregate>.Command => _command;

        async Task<IResult<ICommandResult<TAggregate>>> IOperation<TAggregate>.Evaluate()
        {
            var result = await _aggregate.Evaluate(_command);

            SignalEvaluation(result);

            return result;
        }

        private void SignalEvaluation(IResult<ICommandResult<TAggregate>> result)
        {
            if (result.IsSuccess)
            {
                Result = result.Value;
            }
        }

        void IOperation<TAggregate>.SignalEvaluation(IResult<ICommandResult<TAggregate>> result) => SignalEvaluation(result);
    }

    public sealed class Operation<TAggregate, TResult> : IOperation<TAggregate, TResult>
        where TAggregate : class, IAggregate<TAggregate>
        where TResult : class, ICommandResult<TAggregate>
    {
        private readonly TAggregate _aggregate;
        private readonly ICommand<TAggregate, TResult> _command;

        public Operation(TAggregate aggregate, ICommand<TAggregate, TResult> command)
        {
            _aggregate = aggregate;
            _command = command;
        }

        public TAggregate Aggregate => _aggregate;
        public TResult Result { get; private set; }

        ICommand<TAggregate, TResult> IOperation<TAggregate, TResult>.Command => _command;

        async Task<IResult<TResult>> IOperation<TAggregate, TResult>.Evaluate()
        {
            var result = await _aggregate.Evaluate(_command);

            SignalEvaluation(result);

            return result;
        }

        private void SignalEvaluation(IResult<TResult> result)
        {
            if (result.IsSuccess)
            {
                Result = result.Value;
            }
        }

        void IOperation<TAggregate, TResult>.SignalEvaluation(IResult<TResult> result) => SignalEvaluation(result);
    }
}
