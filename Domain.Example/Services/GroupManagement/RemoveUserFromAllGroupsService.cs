using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate;

namespace Domain.Example.Services.GroupManagement
{
    public class RemoveUserFromAllGroupsService : IService<Guid>
    {
        private readonly IRepository<User> _userRepo;
        private readonly RemoveUserFromGroupService _userRemovalService;

        public RemoveUserFromAllGroupsService(
            IRepository<User> userRepo,
            RemoveUserFromGroupService userRemovalService)
        {
            _userRepo = userRepo;
            _userRemovalService = userRemovalService;
        }

        public async Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(Guid args)
        {
            var user = await _userRepo.ById(args);

            var factory = new CommitPackagesFactory();

            foreach (var groupId in user.Groups)
            {
                /*
                 * Rather than taking the current service one may create a dependency on the dependency
                 * container, ask for a reference of the required service, and issue commands to a different
                 * service instead.
                 */

                factory.AddService(_userRemovalService.Evaluate((groupId, user.Id)));
            }

            return await factory.Evaluate();
        }
    }
}
