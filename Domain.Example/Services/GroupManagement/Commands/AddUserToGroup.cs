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

        /*
         * ToDo: (Plans for the future)
         * - An IServiceCommand implementation should not be responsible for the results of the commit packages
         *   and should instead just return a list of commit packages.
         * - The service responsible for the evaluation of this service command should take the commit packages
         *   and evaluate their correctness.
         */
        async Task<IResult<IEnumerable<ICommitPackage>>> IServiceCommand<GroupManagementService>.Evaluate(GroupManagementService handler)
        {
            var group = handler.GroupRepository.ById(GroupId);
            var user = handler.UserRepository.ById(UserId);

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
                .EvaluateOperation();
        }
    }
}
