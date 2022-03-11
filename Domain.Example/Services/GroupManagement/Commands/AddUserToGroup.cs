using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Components.Extensions;
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
            var group = handler.GroupRepository.ById(GroupId);
            var user = handler.UserRepository.ById(UserId);

            var package = new CommitPackageBuilder<User>()
                .IncludeCommand(
                    new AddGroup
                    {
                        GroupId = GroupId,
                        Name = group.Name
                    });

            // ToDo: Create a helper which takes the aggregate, the commands to be applied, evaluates them, and returns the commit package wrappen in a result
            var userResult = await user.Evaluate(new AddGroup
            {
                GroupId = GroupId,
                Name = group.Name
            });

            var groupResult = await group.Evaluate(new AddUser
            {
                UserId = UserId,
                Name = user.Name
            });

            // ToDo: Allow merging both results through a helper method
            if (userResult.IsFailed || groupResult.IsFailed)
                return new DomainResult<IEnumerable<ICommitPackage>>()
                    .WithReasons(userResult.Reasons)
                    .WithReasons(groupResult.Reasons);

            return DomainResult.Ok(
                new ICommitPackage[]
                {
                    new CommitPackage<User>(user, new [] { userResult.Value }),
                    new CommitPackage<Group>(group, new [] { groupResult.Value })
                });
        }
    }
}
