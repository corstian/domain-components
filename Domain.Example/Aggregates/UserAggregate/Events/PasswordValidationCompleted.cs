using Domain.Components;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class PasswordValidationCompleted : Event<User>
    {
        internal PasswordValidationCompleted() { }

        public bool Succeeded { get; init; }

        public override void Apply(User state)
        {
            state._loginAttempts.Add((DateTime.UtcNow, Succeeded));
        }
    }
}
