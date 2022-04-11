namespace Domain.Components.Tests.Mocks
{
    public class AuthSpecMock : AuthSpec<TestAggregate, in UserMock>
    {
        public AuthSpecMock(UserMock context) : base(context, (aggregate, context) => context.Name == "John Doe")
        {
        }
    }
}
