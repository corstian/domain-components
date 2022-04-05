using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example.Services.GroupManagement
{
    public class RemoveUserFromGroupService : IServiceCommand<(Guid groupId, Guid userId)>
    {
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<User> _userRepo;
        
        public RemoveUserFromGroupService(
            IRepository<Group> groupRepo,
            IRepository<User> userRepo) {
            _groupRepo = groupRepo;
            _userRepo = userRepo;
        }

        public async Task<IResult<IEnumerable<ICommitPackage>>> Evaluate((Guid groupId, Guid userId) args)
        {
            var group = await _groupRepo.ById(args.groupId);
            var user = await _userRepo.ById(args.userId);

            var _factory = new CommitPackagesFactory();

            _factory.AddCommitPackage(group, builder => builder
                .IncludeCommand(new RemoveUser
                {
                    UserId = args.userId
                }));

            _factory.AddCommitPackage(user, builder => builder
                .IncludeCommand(new RemoveGroup
                {
                    GroupId = args.groupId
                }));

            return await _factory.Evaluate();
        }
    }
}
