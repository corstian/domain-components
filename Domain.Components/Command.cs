using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Command : ICommand
    {

    }

    public abstract class AuthorizedCommand<T> : ICommand<T>
        where T : IAggregate<T>
    {
        internal readonly AuthSpec<T>? AuthSpec;

        public AuthorizedCommand(AuthSpec<T>? authSpec)
        {
            AuthSpec = authSpec;
        }

        public abstract IResult<IEnumerable<IEvent<T>>> Evaluate(T handler);

        IResult<IEnumerable<IEvent<T>>> ICommand<T>.Evaluate(T handler)
        {
            if (AuthSpec?.IsSatisfiedBy(handler) ?? false)
            {
                var result = Evaluate(handler);

                if (result.IsFailed) return result;
                
                foreach (var @event in result.Value)
                {
                    if (@event is Event e)
                        e.AuthorizationContext = AuthSpec.AuthorizationContext;
                }

                return result;
            }

            return DomainResult.Fail<IEnumerable<IEvent<T>>>("Unauthorized");
        }
    }
}
