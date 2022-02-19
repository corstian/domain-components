using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Xunit;

namespace Domain.Components.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var command = new ChangeEmail
            {
                Email = "john.doe@example.com"
            };

            Assert.True(command is ICommand<User, IEvent<User>>);
        }
    }
}