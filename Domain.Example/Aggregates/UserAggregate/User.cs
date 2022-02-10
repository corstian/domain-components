using Domain.Components;
using System.Net;

namespace Domain.Example.Aggregates.UserAggregate
{
    public class User : Aggregate<User>
    {
        public string Name { get; internal set; }
        public string Email { get; internal set; }

        public byte[] PasswordSalt { get; internal set; }
        public byte[] PasswordHash { get; internal set; }

        internal List<(DateTime, bool)> _loginAttempts = new List<(DateTime, bool)>();
        public IReadOnlyList<(DateTime, bool)> LoginAttempts => _loginAttempts.AsReadOnly();

        internal List<Guid> _groups = new List<Guid>();
        public IReadOnlyList<Guid> Groups => _groups.AsReadOnly();
    }
}
