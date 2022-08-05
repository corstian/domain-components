﻿using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Components.Extensions;
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

            public Task Apply(ICommandResult<User> commandResult)
                => User.Apply(commandResult);

            public Task<IResult<ICommandResult<User>>> Evaluate(ICommand<User> command)
                => User.Evaluate(command);

            public ValueTask<string> GetIdentity()
                => ValueTask.FromResult(User.Id.ToString());

            Task IAggregate<User>.Apply(IEvent<User> @event)
                => User.Apply(@event);

            Task IAggregate<User>.Apply(params IEvent<User>[] events)
                => User.Apply(events);

            Task<TModel> IAggregate<User>.GetSnapshot<TModel>()
                => User.GetSnapshot<TModel>();

            public async Task<IEnumerable<IResult<ICommandResult<User>>>> Evaluate(params ICommand<User>[] commands)
            {
                var results = new List<IResult<ICommandResult<User>>>();
                foreach (var command in commands)
                    results.Add(await Evaluate(command));
                return results;
            }
        }

        [Fact]
        public async Task ProxyingShouldWork()
        {
            var aggregate = new UserProxy();

            var command = new Rename { Name = "John" };

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

            Assert.Equal(aggregate.User.Id, result.Value.Renamed.AggregateId);
            Assert.Equal("John Doe", result.Value.Renamed.Name);

            Assert.Equal(aggregate.User.Id, result.Value.EmailChanged.AggregateId);
            Assert.Equal("john.doe@example.com", result.Value.EmailChanged.Email);
        }
    }
}
