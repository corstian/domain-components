using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Services;
using Domain.Example.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests.Services
{
    public class AddUserToGroupTests
    {
        IServiceProvider serviceProvider = new ServiceCollection()
            .AddSingleton<IRepository<Group>>((serviceProvider) => new MockRepository<Group>())
            .AddSingleton<IRepository<User>>((serviceProvider) => new MockRepository<User>())
            .AddSingleton<IServiceEvaluator>((serviceProvider) => new BatchedServiceEvaluator(serviceProvider))
            .BuildServiceProvider();

        [Fact]
        public async Task CanAddUserToGroup()
        {
            var userId = Guid.NewGuid();
            var groupId = Guid.NewGuid();

            var service = new AddUserToGroupService
            {
                GroupId = groupId,
                UserId = userId
            };

            var serviceEvaluator = serviceProvider.GetRequiredService<IServiceEvaluator>();

            var result = await serviceEvaluator.Evaluate(service);

            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(userId, result.Value.AddUserEvent.Result.UserId);
            Assert.Equal(groupId, result.Value.AddUserEvent.Result.AggregateId);
            Assert.Equal(groupId, result.Value.AddGroupEvent.Result.GroupId);
            Assert.Equal(userId, result.Value.AddGroupEvent.Result.AggregateId);
        }
    }
}
