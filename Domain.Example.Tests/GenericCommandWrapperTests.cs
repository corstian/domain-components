using Domain.Components;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using System;
using Xunit;

namespace Domain.Example.Tests
{
    public class GenericCommandWrapperTests
    {
        [Fact]
        public void SingleTypedCommand()
        {
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var command = new ChangeEmail
            {
                Email = "john.doe@example.com"
            };

            var result = user.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(user.Id, result.Value.AggregateId);
        }

        [Fact]
        public void DoubleTypedCommand()
        {
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var command = new ChangeInfo
            {
                Name = "John Doe",
                Email = "john.doe@example.com"
            };

            var result = user.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(user.Id, result.Value.Item1.AggregateId);
            Assert.Equal(user.Id, result.Value.Item2.AggregateId);
        }
    }
}
