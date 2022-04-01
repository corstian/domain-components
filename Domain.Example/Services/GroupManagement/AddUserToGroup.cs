using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example.Services.GroupManagement
{
    public class AddUserToGroup : IServiceCommand<GroupManagementService>
    {
        public Guid GroupId { get; init; }
        public Guid UserId { get; init; }

        async Task<IResult<IEnumerable<ICommitPackage>>> IServiceCommand<GroupManagementService>.Evaluate(GroupManagementService handler)
        {
            var group = await handler.GroupRepository.ById(GroupId);
            var user = await handler.UserRepository.ById(UserId);

            return await new CommitPackagesFactory()
                .AddCommitPackage<Group>(group, groupBuilder => groupBuilder
                    .IncludeCommand(new AddUser
                    {
                        UserId = UserId,
                        Name = user.Name
                    }))
                .AddCommitPackage<User>(user, userBuilder => userBuilder
                    .IncludeCommand(new AddGroup
                    {
                        GroupId = GroupId,
                        Name = group.Name
                    }))
                .Evaluate();
        }
    }
}
