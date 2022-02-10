using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Snapshots
{
    public class PublicUserInfo : ISnapshot<User>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }

        void ISnapshot<User>.Populate(User aggregate)
        {
            Name = aggregate.Name;
            Email = aggregate.Email;
        }
    }
}
