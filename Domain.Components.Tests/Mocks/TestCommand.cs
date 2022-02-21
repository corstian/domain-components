namespace Domain.Components.Tests.Mocks
{
    public class TestCommand : Command<TestAggregate, TestEvent>
    {
        public override DomainResult<TestEvent> Evaluate(TestAggregate handler)
            => DomainResult.Ok(new TestEvent());
    }
}
