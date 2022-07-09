using Domain.Components.Extensions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class GenericCommandWrapperTests
    {
        [Fact]
        public async Task SingleTypedCommand()
        {
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var command = new ChangeEmail
            {
                Email = "john.doe@example.com"
            };

            var result = await user.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(user.Id, result.Value.AggregateId);
        }

        [Fact]
        public async Task DoubleTypedCommand()
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

            var result = await user.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(user.Id, result.Value.Renamed.AggregateId);
            Assert.Equal(user.Id, result.Value.EmailChanged.AggregateId);
        }
    }
}
