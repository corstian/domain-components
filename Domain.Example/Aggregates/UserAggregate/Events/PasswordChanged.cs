using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordChanged : Event, IEvent<User>
    {
        internal PasswordChanged() { }

        public byte[] PasswordSalt { get; init; }
        public byte[] PasswordHash { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state.PasswordHash = PasswordHash;
            state.PasswordSalt = PasswordSalt;
        }
    }
}
