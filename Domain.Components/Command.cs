using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Command
    {
        
    }

    public abstract class AuthorizedCommand<T> : ICommand<T>
        where T : class, IAggregate<T>
    {
        internal readonly AuthSpec<T>? AuthSpec;

        public AuthorizedCommand(AuthSpec<T>? authSpec)
        {
            AuthSpec = authSpec;
        }

        public abstract IResult<ICommandResult<T>> Evaluate(T handler);

        IResult<ICommandResult<T>> ICommand<T>.Evaluate(T aggregate)
        {
            if (AuthSpec?.IsSatisfiedBy(aggregate) ?? false)
            {
                var result = Evaluate(aggregate);

                if (result.IsFailed) return result;

                foreach (var @event in result.Value.Events)
                {
                    if (@event is Event e)
                        e.AuthorizationContext = AuthSpec.AuthorizationContext;
                }

                return result;
            }

            return DomainResult.Fail<ICommandResult<T>>("Unauthorized");
        }
    }
}
