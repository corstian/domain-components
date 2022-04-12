using Domain.Components.Abstractions;
using System.Collections.Generic;

namespace Domain.Components.Tests.Mocks
{
    public class AuthorizedTestCommand : AuthorizedCommand<TestAggregate>
    {
        public AuthorizedTestCommand(AuthSpec<TestAggregate>? authSpec) : base(authSpec)
        {
        }

        public override IResult<IEnumerable<IEvent<TestAggregate>>> Evaluate(TestAggregate handler)
            => DomainResult.Ok<IEnumerable<IEvent<TestAggregate>>>(new[] { new TestEvent() });
    }
}
