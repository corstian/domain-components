using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.GroupAggregate.Events;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Services
{
    public class AddUserToGroupService : IService<(Guid userId, Guid groupId), AddUserToGroupService.Result>
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Group> _groupRepo;

        public AddUserToGroupService(
            IRepository<User> userRepo,
            IRepository<Group> groupRepo)
        {
            _userRepo = userRepo;
            _groupRepo = groupRepo;
        }

        public async Task<IResult<Result>> Invoke((Guid userId, Guid groupId) tuple)
        {
            var user = await _userRepo.ById(tuple.userId);
            var group = await _groupRepo.ById(tuple.groupId);

            var userCommand = new AddGroup { 
                GroupId = group.Id,
                Name = group.Name
            };

            var groupCommand = new AddUser { 
                UserId = user.Id,
                Name = user.Name
            };

            var userResult = await user.Evaluate(userCommand);
            var groupResult = await group.Evaluate(groupCommand);

            return new DomainResult<Result>()
                .WithValue(new Result
                {
                    AddGroupEvent = userResult.Value,
                    AddUserEvent = groupResult.Value
                })
                .WithReasons(userResult.Reasons.Concat(groupResult.Reasons));
        }

        public class Result : IServiceResult
        {
            public IEnumerable<IMarkCommandOutput> Results => new IMarkCommandOutput[] { AddGroupEvent, AddUserEvent }; 

            public GroupAdded AddGroupEvent { get; init; }
            public UserAdded AddUserEvent { get; init; }
        }
    }
}
