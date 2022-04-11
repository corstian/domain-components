using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Command : ICommand
    {

    }

    public abstract class Command<T>
        where T : IAggregate<T>
    {
        internal readonly AuthSpec<T, IAuthorizationContext>? AuthSpec;

        public Command() { }

        public Command(AuthSpec<T, IAuthorizationContext>? authSpec)
        {
            AuthSpec = authSpec;
        }
    }
}
