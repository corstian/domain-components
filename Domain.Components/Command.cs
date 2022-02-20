using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Command<T> : ICommand<T>
        where T : IAggregate<T>
    {
        internal readonly AuthSpec<object>? _authSpec;
        public Command(AuthSpec<object>? authSpec = null)
        {
            _authSpec = authSpec;
        }

        //public abstract IEnumerable<IEvent<T>> Evaluate(T handler);
        //public abstract Result Validate(T handler);
        public abstract IResult<IEnumerable<IEvent<T>>> Evaluate(T handler);
    }

    public abstract class Command<T, E> : ICommand<T, E>
        where T : IAggregate<T>
        where E : IEvent<T>
    {
        internal readonly AuthSpec<object>? _authSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            _authSpec = authSpec;
        }

        public abstract DomainResult<E> Evaluate(T handler);

        IResult<E> ICommand<T, E>.Evaluate(T handler)
            => Evaluate(handler);

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
            => Evaluate(handler).ToResult((e) => new IEvent<T>[] { e }.AsEnumerable());
    }

    public abstract class Command<T, E1, E2> : ICommand<T, E1, E2>
        where T : IAggregate<T>
        where E1 : IEvent<T>
        where E2 : IEvent<T>
    {
        internal readonly AuthSpec<object>? _authSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            _authSpec = authSpec;
        }

        public abstract DomainResult<(E1, E2)> Evaluate(T handler);

        IResult<(E1, E2)> ICommand<T, E1, E2>.Evaluate(T handler)
            => Evaluate(handler);

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
            => Evaluate(handler).ToResult((result) => new IEvent<T>[] { result.Item1, result.Item2 }.AsEnumerable());
    }
}
