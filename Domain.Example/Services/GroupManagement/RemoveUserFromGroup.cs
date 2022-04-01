using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example.Services.GroupManagement
{
    public class RemoveUserFromGroup : ServiceCommand, IServiceCommand<GroupManagementService>
    {
        public Guid GroupId { get; init; }
        public Guid UserId { get; init; }

        async Task<IResult<IEnumerable<ICommitPackage>>> IServiceCommand<GroupManagementService>.Evaluate(GroupManagementService service)
        {
            var group = await service.GroupRepository.ById(GroupId);
            var user = await service.UserRepository.ById(UserId);

            AddressAggregate(group, builder => builder
                .IncludeCommand(new RemoveUser
                {
                    UserId = UserId
                }));

            AddressAggregate(user, builder => builder
                .IncludeCommand(new RemoveGroup
                {
                    GroupId = GroupId
                }));

            return await Evaluate();
        }
    }
}
