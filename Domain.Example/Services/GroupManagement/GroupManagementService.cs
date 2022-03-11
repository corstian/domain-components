using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.UserAggregate;

namespace Domain.Example.Services.GroupManagement
{
    public class GroupManagementService : Service<GroupManagementService>
    {
        internal readonly IRepository<Group> GroupRepository;
        internal readonly IRepository<User> UserRepository;

        public GroupManagementService(
            IRepository<Group> groupRepository,
            IRepository<User> userRepository)
        {
            GroupRepository = groupRepository;
            UserRepository = userRepository;
        }
    }
}
