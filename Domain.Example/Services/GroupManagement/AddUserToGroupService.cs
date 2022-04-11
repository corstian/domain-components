using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example.Services.GroupManagement
{
    public class AddUserToGroupService : IService<(Guid groupId, Guid userId)>
    {
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<User> _userRepo;

        public AddUserToGroupService(
            IRepository<Group> groupRepo,
            IRepository<User> userRepo)
        {
            _groupRepo = groupRepo;
            _userRepo = userRepo;
        }

        public async Task<IResult<IEnumerable<ICommitPackage>>> Evaluate((Guid groupId, Guid userId) args)
        {
            var group = await _groupRepo.ById(args.groupId);
            var user = await _userRepo.ById(args.userId);

            return await new CommitPackagesFactory()
                .AddCommitPackage<Group>(group, groupBuilder => groupBuilder
                    .IncludeCommand(new AddUser
                    {
                        UserId = args.userId,
                        Name = user.Name
                    }))
                .AddCommitPackage<User>(user, userBuilder => userBuilder
                    .IncludeCommand(new AddGroup
                    {
                        GroupId = args.groupId,
                        Name = group.Name
                    }))
                .Evaluate();
        }
    }
}
