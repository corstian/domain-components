using Domain.Components.Abstractions;

namespace Domain.Components.Tests.Mocks
{
    public class UserMock : IAuthorizationContext
    {
        public string Name { get; set; }
    }
}
