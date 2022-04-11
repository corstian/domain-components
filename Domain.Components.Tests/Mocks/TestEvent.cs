using Domain.Components.Abstractions;

namespace Domain.Components.Tests.Mocks
{
    public class TestEvent : Event, IEvent<TestAggregate>
    {
        void IEvent<TestAggregate>.Apply(TestAggregate state)
        {
            state.EventsApplied = state.EventsApplied + 1;
        }
    }
}
