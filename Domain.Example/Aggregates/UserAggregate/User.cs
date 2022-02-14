using Domain.Components;

namespace Domain.Example.Aggregates.UserAggregate
{
    public class User : Aggregate<User>
    {
        public string Name { get; internal set; }
        public string Email { get; internal set; }

        internal byte[] PasswordSalt { get; set; }
        internal byte[] PasswordHash { get; set; }

        internal List<(DateTime, bool)> _loginAttempts = new();
        public IReadOnlyList<(DateTime, bool)> LoginAttempts => _loginAttempts.AsReadOnly();

        internal List<Guid> _groups = new();
        public IReadOnlyList<Guid> Groups => _groups.AsReadOnly();
    }
}
