using Domain.Components.Abstractions;
using Domain.Components.Tests.Mocks;
using Xunit;

namespace Domain.Components.Tests
{
    public class TypeMatchingTests
    {
        [Fact]
        public void AbstractCommandShouldDeriveFromInterfaces()
        {
            var command = new TestCommand();

            Assert.True(command is ICommand<TestAggregate, IEvent<TestAggregate>>);
        }

        [Fact]
        public void AbstractEventShouldDeriveFromInterfaces()
        {
            var @event = new TestEvent();

            Assert.True(@event is IEvent<TestAggregate>);
            Assert.True(@event is IEvent);
            Assert.True(@event is Event<TestAggregate>);
            Assert.True(@event is Event);
        }
    }
}
