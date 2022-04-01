using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Services.GroupManagement
{
    public class RemoveUserFromAllGroups : IServiceCommand<GroupManagementService>
    {
        public Guid UserId { get; init; }

        public async Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(GroupManagementService service)
        {
            var user = await service.UserRepository.ById(UserId);

            var factory = new CommitPackagesFactory();

            foreach (var groupId in user.Groups)
            {
                /*
                 * Rather than taking the current service one may create a dependency on the dependency
                 * container, ask for a reference of the required service, and issue commands to a different
                 * service instead.
                 */

                factory.AddServiceCommand(
                    service, 
                    new RemoveUserFromGroup
                    {
                        UserId = user.Id,
                        GroupId = groupId
                    });
            }

            return await factory.Evaluate();
        }
    }
}
