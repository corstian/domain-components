using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Events;
using Xunit;

namespace Domain.Example.Tests
{
    public class TypeMatchingTest
    {
        [Fact]
        public void Test1()
        {
            var command = new ChangeEmail
            {
                Email = "john@example.com"
            };

            Assert.True(command is ICommand<User, EmailChanged>);
            Assert.True(command is ICommand<User, IEvent<User>>);
            Assert.True(command is ICommand<User>);
        }
    }
}
