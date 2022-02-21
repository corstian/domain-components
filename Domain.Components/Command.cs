namespace Domain.Components
{
    public abstract class Command
    {
        internal readonly AuthSpec<object>? AuthSpec;

        public Command(AuthSpec<object>? authSpec = null)
        {
            AuthSpec = authSpec;
        }
    }
}
