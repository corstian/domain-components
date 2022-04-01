using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.ProcessManagers
{
    public class UserRegistrationProcess : LongRunningProcess<UserRegistrationProcess>,
        IHandle<Renamed, AggregateIdEventReducer>,
        IHandle<EmailChanged, AggregateIdEventReducer>
    {
        public UserRegistrationProcess(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public async Task Process(Renamed @event)
        {
            var user = await GetRepository<User>().ById(@event.AggregateId);
        }

        public Task Process(EmailChanged @event)
        {
            return Task.CompletedTask;
        }
    }
}
