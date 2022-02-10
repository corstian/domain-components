using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordChanged : IEvent<User>
    {
        public PasswordChanged() { }

        public byte[] PasswordSalt { get; init; }
        public byte[] PasswordHash { get; init; }

        Guid IEvent.AggregateId => throw new NotImplementedException();

        void IEvent<User>.Apply(User state)
        {
            state.PasswordHash = PasswordHash;
            state.PasswordSalt = PasswordSalt;
        }
    }
}
