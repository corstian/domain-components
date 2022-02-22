using Domain.Components.Abstractions;

namespace Domain.Components
{
    internal class GenericCommandWrapper<T, E> : ICommand<T>, ICommand<T, E>
        where T : IAggregate<T>
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
                .WithReasons(result.Reasons)
                .WithValue(new IEvent<T>[] 
                { 
                    result.ValueOrDefault 
                });
        }

        IResult<E> ICommand<T, E>.Evaluate(T handler)
            => _command.Evaluate(handler);
    }

    internal class GenericCommandWrapper<T, E1, E2> : ICommand<T>, ICommand<T, E1, E2>
        where T : IAggregate<T>
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
                .WithReasons(result.Reasons)
                .WithValue(new IEvent<T>[]
                {
                    result.ValueOrDefault.Item1,
                    result.ValueOrDefault.Item2
                });
        }

        IResult<(E1, E2)> ICommand<T, E1, E2>.Evaluate(T handler)
            => _command.Evaluate(handler);
    }
}
