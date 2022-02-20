using Domain.Components;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordChanged : Event<User>
    {
        internal PasswordChanged() { }

        public byte[] PasswordSalt { get; init; }
        public byte[] PasswordHash { get; init; }

        public override void Apply(User state)
        {
            state.PasswordHash = PasswordHash;
            state.PasswordSalt = PasswordSalt;
        }
    }
}
