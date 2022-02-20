using Domain.Components;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class Rename : Command<User, Renamed>
    {
        public string Name { get; init; }

        public override DomainResult<Renamed> Evaluate(User handler)
        {
            return DomainResult.Ok(new Renamed
            {
                Name = Name
            });
        }
    }
}
