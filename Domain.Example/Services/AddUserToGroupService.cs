using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Components.Extensions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.GroupAggregate.Events;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Example.Services
{
    public class AddUserToGroupService : IService<AddUserToGroupService.Result>
    {
        public Guid UserId { get; init; }
        public Guid GroupId { get; init; }

        public async Task<IResult<IServicePromise<Result>>> Stage(IServiceProvider serviceProvider)
        {
            var userRepo = serviceProvider.GetService<IRepository<User>>();
            var groupRepo = serviceProvider.GetService<IRepository<Group>>();

            var user = await userRepo.ById(UserId);
            var group = await groupRepo.ById(GroupId);


            return new DomainResult<Result>()
                .WithValue(new Result
                {
                    AddUserEvent = group.LazilyEvaluate(new AddUser
                    {
                        UserId = user.Id,
                        Name = user.Name
                    }),
                    AddGroupEvent = user.LazilyEvaluate(new AddGroup
                    {
                        GroupId = group.Id,
                        Name = group.Name
                    })
                });
        }

        public class Result : IServiceResult<Result>
        {
            public IStagedCommand<User, GroupAdded> AddGroupEvent { get; init; }
            public IStagedCommand<Group, UserAdded> AddUserEvent { get; init; }

            IEnumerable<IServiceResult> IServiceResult.Operations => new IServiceResult[] { AddGroupEvent, AddUserEvent };
        }
    }
}
