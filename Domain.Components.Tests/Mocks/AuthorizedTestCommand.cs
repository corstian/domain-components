using Domain.Components.Abstractions;

namespace Domain.Components.Tests.Mocks
{
    public class AuthorizedTestCommand : Command<TestAggregate>, ICommand<TestAggregate, TestEvent>
    {
        public AuthorizedTestCommand(AuthSpec<TestAggregate, IAuthorizationContext>? authSpec) : base(authSpec)
        {
        }

        IResult<TestEvent> ICommand<TestAggregate, TestEvent>.Evaluate(TestAggregate handler)
            => DomainResult.Ok(new TestEvent());
    }
}
