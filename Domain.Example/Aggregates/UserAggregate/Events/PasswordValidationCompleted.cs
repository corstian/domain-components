using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordValidationCompleted : Event<User>, IEvent<User>
    {
        internal PasswordValidationCompleted() { }

        public bool Succeeded { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state._loginAttempts.Add((DateTime.UtcNow, Succeeded));
        }
    }
}
