using Domain.Components.Abstractions;
using FluentResults;

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
        where E : Event<T>
    {
        internal readonly AuthSpec<object>? _authSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            _authSpec = authSpec;
        }

        //public abstract E Evaluate(T handler);
        //public abstract Result Validate(T handler);

        public abstract IResult<E> Evaluate(T handler);
    }

    public abstract class Command<T, E1, E2> : ICommand<T, E1, E2>
        where T : IAggregate<T>
        where E1 : Event<T>
        where E2 : Event<T>
    {
        internal readonly AuthSpec<object>? _authSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            _authSpec = authSpec;
        }

        //public abstract (E1, E2) Evaluate(T handler);
        //public abstract Result Validate(T handler);

        public abstract IResult<(E1, E2)> Evaluate(T handler);
    }
}
