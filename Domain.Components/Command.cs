using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Command : ICommand
    {
        internal readonly AuthSpec<object>? AuthSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            AuthSpec = authSpec;
        }
    }
}
