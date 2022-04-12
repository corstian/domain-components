namespace Domain.Components.Tests.Mocks
{
    public class AuthSpecMock : AuthSpec<TestAggregate>
    {
        public AuthSpecMock(UserMock context) : base(context, (aggregate, context) => ((UserMock)context).Name == "John Doe")
        {
        }
    }
}
