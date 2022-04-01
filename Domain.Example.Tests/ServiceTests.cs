using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Services;
using Domain.Example.Services.GroupManagement;
using Domain.Example.Tests.Mocks;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class ServiceTests
    {
        [Fact]
        public async Task GroupAddAndRemoveUserTest()
        {
            var groupRepo = new MockRepository<Group>();
            var userRepo = new MockRepository<User>();

            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();

            var service = new GroupManagementService(
                groupRepo,
                userRepo);

            var addedResult = await service
                .Evaluate(new AddUserToGroup
                {
                    UserId = userId,
                    GroupId = groupId
                });

            Assert.True(addedResult.IsSuccess);

            foreach (var package in addedResult.Value)
                await package.Apply();

            Assert.Equal(2, addedResult.Value.Count());

            var user = await userRepo.ById(userId);

            Assert.NotEmpty(user.Groups);
            Assert.Equal(groupId, user.Groups[0]);

            var group = await groupRepo.ById(groupId);

            Assert.NotEmpty(group.Members);
            Assert.Equal(userId, group.Members[0]);

            // And clear our results up

            var removalResult = await service
                .Evaluate(new RemoveUserFromGroup
                {
                    UserId = userId,
                    GroupId = groupId
                });

            Assert.True(removalResult.IsSuccess);

            foreach (var package in removalResult.Value)
                await package.Apply();

            Assert.Empty(user.Groups);
            Assert.Empty(group.Members);
        }
        
        [Fact]
        public async Task RemoveUserFromAllGroups()
        {
            var userRepo = new MockRepository<User>();
            var groupRepo = new MockRepository<Group>();

            var service = new GroupManagementService(groupRepo, userRepo);

            var userId = Guid.NewGuid();

            for (var i = 0; i < 10; i++)
            {
                var result = await service.Evaluate(new AddUserToGroup
                {
                    UserId = userId,
                    GroupId = Guid.NewGuid()
                });

                if (result.IsSuccess)
                    foreach (var package in result.Value)
                        await package.Apply();
            }

            var user = await userRepo.ById(userId);

            Assert.True(user.Groups.Count() == 10);
            Assert.True(user.Groups.Distinct().Count() == 10);

            var clearingPackages = await service.Evaluate(new RemoveUserFromAllGroups
            {
                UserId = userId
            });

            if (clearingPackages.IsSuccess)
                foreach (var package in clearingPackages.Value)
                    await package.Apply();

            Assert.Empty(user.Groups);
        }
    }
}
