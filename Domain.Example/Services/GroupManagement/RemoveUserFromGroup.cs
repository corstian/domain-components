using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example.Services.GroupManagement
{
    public class RemoveUserFromGroup : IServiceCommand<GroupManagementService>
    {
        public Guid GroupId { get; init; }
        public Guid UserId { get; init; }

        async Task<IResult<IEnumerable<ICommitPackage>>> IServiceCommand<GroupManagementService>.Evaluate(GroupManagementService service)
        {
            var group = service.GroupRepository.ById(GroupId);
            var user = service.UserRepository.ById(UserId);

            return await new CommitPackagesFactory()
                .AddCommitPackage(group, groupBuilder => groupBuilder
                    .IncludeCommand(new RemoveUser
                    {
                        UserId = UserId
                    }))
                .AddCommitPackage<User>(user, userBuilder => userBuilder
                    .IncludeCommand(new RemoveGroup
                    {
                        GroupId = GroupId
                    }))
                .Evaluate();
        }
    }
}
