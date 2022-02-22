using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class InterfaceUsageTests
    {
        internal class UserProxy : IAggregate<User>
        {
            public User User = new User
            {
                Id = Guid.NewGuid()
            };

            Task IAggregate<User>.Apply(IEvent<User> @event)
                => User.Apply(@event);

            Task IAggregate<User>.Apply(params IEvent<User>[] events)
                => User.Apply(events);

            Task<TModel> IAggregate<User>.Apply<TModel>(IEvent<User> @event)
                => User.Apply<TModel>(@event);

            Task<TModel> IAggregate<User>.Apply<TModel>(params IEvent<User>[] events)
                => User.Apply<TModel>(events);

            Task<IResult<IEnumerable<IEvent<User>>>> IAggregate<User>.Evaluate(ICommand<User> command)
                => User.Evaluate(command);
        }

        [Fact]
        public async Task ProxyingShouldWork()
        {
            var aggregate = new UserProxy();

            var command = new Rename();

            var result = await aggregate.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(aggregate.User.Id, result.Value.AggregateId);
        }

        [Fact]
        public async Task ProxyingDoublyTypedCommandShouldWork()
        {
            var aggregate = new UserProxy();
            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var result = await aggregate.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(aggregate.User.Id, result.Value.Item1.AggregateId);
            Assert.Equal("John Doe", result.Value.Item1.Name);
            Assert.Equal(aggregate.User.Id, result.Value.Item2.AggregateId);
            Assert.Equal("john.doe@example.com", result.Value.Item2.Email);
        }
    }
}
