using Domain.Components.Abstractions;

namespace Domain.Components
{
    internal class GenericCommandWrapper<T, R> : ICommand<T>, ICommand<T, R>
        where T : IAggregate
        where R : ICommandResult<T>
    {
        private readonly ICommand<T, R> _command;

        public GenericCommandWrapper(ICommand<T, R> command)
        {
            _command = command;
        }

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
        {
            var result = _command.Evaluate(handler);

            return new DomainResult<IEnumerable<IEvent<T>>>()
                .WithValue(result.IsFailed 
                    ? default 
                    : result.Value.Result)
                .WithReasons(result.Reasons);
        }

        IResult<R> ICommand<T, R>.Evaluate(T handler)
            => _command.Evaluate(handler);
    }
}
