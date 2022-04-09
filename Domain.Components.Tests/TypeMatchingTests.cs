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
            Assert.True(@event is Event);
        }

        [Fact]
        public void AggregateShouldDeriveFromInterface()
        {
            var aggregate = new TestAggregate();

            Assert.True(aggregate is IAggregate);
            Assert.True(aggregate is IAggregate<TestAggregate>);
            Assert.True(aggregate is TestAggregate);
        }

        [Fact]
        public void AggregateProxyShouldMatchInteraces()
        {
            var proxy = new AggregateProxy<TestAggregate>();

            Assert.True(proxy is IAggregate);
            Assert.True(proxy is IAggregate<TestAggregate>);
        }

        [Fact]
        public void CommitPackageTypeTests()
        {
            var commitPackage = new CommitPackage<TestAggregate>();

            Assert.True(commitPackage is ICommitPackage);
            Assert.True(commitPackage is ICommitPackage<TestAggregate>);
        }
    }
}
