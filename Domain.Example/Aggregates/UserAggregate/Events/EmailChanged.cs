using Domain.Components;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class EmailChanged : Event<User>
    {
        internal EmailChanged() { }

        public string Email { get; init; }

        public override void Apply(User state)
        {
            state.Email = Email;
        }
    }
}
