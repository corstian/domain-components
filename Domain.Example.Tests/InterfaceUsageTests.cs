using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Example.Tests
{
    public class InterfaceUsageTests
    {
        internal class UserProxy : IAggregate<User>
        {
            private User _user = new User
            {
                Id = Guid.NewGuid()
            };

            Guid IAggregate.Id => _user.Id;

            void IAggregate<User>.Apply(IEvent<User> @event)
                => _user.Apply(@event);

            void IAggregate<User>.Apply(params IEvent<User>[] events)
                => _user.Apply(events);

            TModel IAggregate<User>.Apply<TModel>(IEvent<User> @event)
                => _user.Apply<TModel>(@event);

            TModel IAggregate<User>.Apply<TModel>(params IEvent<User>[] events)
                => _user.Apply<TModel>(events);

            IResult<IEnumerable<IEvent<User>>> IAggregate<User>.Evaluate(ICommand<User> command)
                => _user.Evaluate(command);
        }

        [Fact]
        public void ProxyingShouldWork()
        {
            IAggregate<User> aggregate = new UserProxy();

            var command = new Rename();

            var result = aggregate.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(aggregate.Id, result.Value.AggregateId);
        }

        [Fact]
        public void ProxyingDoublyTypedCommandShouldWork()
        {
            IAggregate<User> aggregate = new UserProxy();
            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var result = aggregate.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(aggregate.Id, result.Value.Item1.AggregateId);
            Assert.Equal("John Doe", result.Value.Item1.Name);
            Assert.Equal(aggregate.Id, result.Value.Item2.AggregateId);
            Assert.Equal("john.doe@example.com", result.Value.Item2.Email);
        }
    }
}
