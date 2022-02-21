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

            Assert.True(command is ICommand<TestAggregate>);
            Assert.True(command is ICommand<TestAggregate, IEvent<TestAggregate>>);
            Assert.True(command is Command<TestAggregate, TestEvent>);
        }
    }
}
