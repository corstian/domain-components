namespace Domain.Components.Tests.Mocks
{
    public class TestAggregate : Aggregate<TestAggregate>
    {
        public int EventsApplied { get; internal set; } = 0;
    }
}
