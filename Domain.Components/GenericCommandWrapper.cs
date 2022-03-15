using Domain.Components.Abstractions;

namespace Domain.Components
{
    internal class GenericCommandWrapper<T, E> : ICommand<T>, ICommand<T, E>
        where T : IAggregate
        where E : IEvent<T>
    {
        private readonly ICommand<T, E> _command;

        public GenericCommandWrapper(ICommand<T, E> command)
        {
            _command = command;
        }

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
        {
            var result = _command.Evaluate(handler);

            return new DomainResult<IEnumerable<IEvent<T>>>()
                .WithValue(result.IsFailed ? default : new IEvent<T>[]
                {
                    result.Value
                })
                .WithReasons(result.Reasons);
        }

        IResult<E> ICommand<T, E>.Evaluate(T handler)
            => _command.Evaluate(handler);
    }

    internal class GenericCommandWrapper<T, E1, E2> : ICommand<T>, ICommand<T, E1, E2>
        where T : IAggregate
        where E1 : IEvent<T>
        where E2 : IEvent<T>
    {
        private readonly ICommand<T, E1, E2> _command;

        public GenericCommandWrapper(ICommand<T, E1, E2> command)
        {
            _command = command;
        }

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
        {
            var result = _command.Evaluate(handler);

            return new DomainResult<IEnumerable<IEvent<T>>>()
                .WithValue(result.IsFailed ? default : new IEvent<T>[]
                {
                    result.Value.Item1,
                    result.Value.Item2
                })
                .WithReasons(result.Reasons);
        }

        IResult<(E1, E2)> ICommand<T, E1, E2>.Evaluate(T handler)
            => _command.Evaluate(handler);
    }
}
