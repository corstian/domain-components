using Domain.Components;
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
        public void MatchingTypes()
        {
            var command = new ChangeEmail();

            Assert.True(command is ICommand<User, EmailChanged>);
            Assert.True(command is Command);
        }
    }
}
