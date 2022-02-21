using System;

namespace Domain.Components.Tests.Mocks
{
    public class TestEvent : Event<TestAggregate>
    {
        public override void Apply(TestAggregate state)
        {
            throw new NotImplementedException();
        }
    }
}
