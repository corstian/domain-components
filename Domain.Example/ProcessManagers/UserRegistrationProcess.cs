using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.ProcessManagers
{
    public class UserRegistrationProcess :
        IHandle<Renamed>
    {
        [WithFilter<Renamed.JohnDoeFilter>]
        public Task Process(Renamed @event) => throw new NotImplementedException();
    }
}
