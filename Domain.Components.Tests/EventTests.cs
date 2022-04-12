using Domain.Components.Abstractions;
using Domain.Components.Extensions;
using Domain.Components.Tests.Mocks;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Components.Tests
{
    public class EventTests
    {
        [Fact]
        public async Task Event_FromAggregate_HasAggregateId()
        {
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid()
            };

            var result = await aggregate.EvaluateAndApply(new TestCommand());

            Assert.True(result.Value.AggregateId == aggregate.Id);
        }

        [Fact]
        public async Task Event_FromAggregate_HasTimestamp()
        {
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid()
            };

            var result = await aggregate.EvaluateAndApply(new TestCommand());

            Assert.True(result.Value.Timestamp != default);
        }

        [Fact]
        public async Task Event_FromIAggregate_HasNoAggregateId()
        {
            var aggregate = new InterfaceAggregate
            {
                Id = Guid.NewGuid()
            };

            var result = await aggregate.EvaluateAndApply(new InterfaceCommand());

            Assert.True(result.Value.AggregateId == default);
        }

        [Fact]
        public async Task Event_FromAggregate_HasAuthorizationContext()
        {
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid()
            };

            var specification = new AuthSpecMock(new UserMock
            {
                Name = "John Doe"
            });

            var result = await aggregate.EvaluateAndApply(new AuthorizedTestCommand(specification));

            var e = result.Value.First() as Event;

            Assert.NotNull(e);
            Assert.True(e.AuthorizationContext is UserMock);
            Assert.True(((UserMock)e.AuthorizationContext).Name == "John Doe");
        }

        [Fact]
        public async Task Event_FromAggregate_FailsAuthorization()
        {
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid()
            };

            var specification = new AuthSpecMock(new UserMock
            {
                Name = "Jane Doe"
            });

            var result = await aggregate.EvaluateAndApply(new AuthorizedTestCommand(specification));

            Assert.True(result.IsFailed);
        }
    }
}
