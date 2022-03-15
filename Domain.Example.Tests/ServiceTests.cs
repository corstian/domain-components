using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Services.GroupManagement;
using Domain.Example.Tests.Mocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class ServiceTests
    {
        [Fact]
        public async Task AddUserToGroupTest()
        {

            var service = new GroupManagementService(
                new MockRepository<Group>(),
                new MockRepository<User>());

            var addUserToGroup = new AddUserToGroup();

            var result = await service.Evaluate(addUserToGroup);

            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
        }
    }
}
