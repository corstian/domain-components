using System;

namespace Domain.Components.Tests.Mocks
{
    public class TestEvent : Event<TestAggregate>
    {
        public override void Apply(TestAggregate state)
        {
            state.EventsApplied = state.EventsApplied + 1;
        }
    }
}
