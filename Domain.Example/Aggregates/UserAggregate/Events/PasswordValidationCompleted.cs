using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordValidationCompleted : IEvent<User>
    {
        internal PasswordValidationCompleted() { }

        public bool Succeeded { get; init; }

        public Guid AggregateId { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state._loginAttempts.Add((DateTime.UtcNow, Succeeded));
        }
    }
}
