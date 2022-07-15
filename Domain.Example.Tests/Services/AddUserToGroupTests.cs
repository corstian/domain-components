using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Services;
using Domain.Example.Tests.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests.Services
{
    public class AddUserToGroupTests
    {
        [Fact]
        public async Task CanAddUserToGroup()
        {
            IRepository<User> userRepo = new MockRepository<User>();
            IRepository<Group> groupRepo = new MockRepository<Group>();

            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();

            var user = userRepo.ById(userId);
            var group = groupRepo.ById(groupId);

            var service = new AddUserToGroupService(userRepo, groupRepo);

            var result = await service.Invoke((userId, groupId));

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(userId, result.Value.AddUserEvent.UserId);
            Assert.Equal(groupId, result.Value.AddUserEvent.AggregateId);
            Assert.Equal(groupId, result.Value.AddGroupEvent.GroupId);
            Assert.Equal(userId, result.Value.AddGroupEvent.AggregateId);
        }
    }
}
