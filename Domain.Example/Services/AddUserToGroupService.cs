using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Components.Extensions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.GroupAggregate.Events;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate.Events;
using Domain.Example.Aggregates.UserAggregate.Snapshots;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Example.Services
{
    public class AddUserToGroupService : IService<AddUserToGroupService.Result>
    {
        public Guid UserId { get; init; }
        public Guid GroupId { get; init; }

        public async Task<IResult<IPromise<Result>>> Stage(IServiceProvider serviceProvider)
        {
            var aggregateProvider = serviceProvider.GetRequiredService<IAggregateProvider>();

            var user = aggregateProvider.Get<User>(UserId);
            var group = aggregateProvider.Get<Group>(GroupId);

            var userInfo = await user.GetSnapshot<PublicUserInfo>();

            var result = new Result
            {
                AddUserEvent = group.LazilyEvaluate(new AddUser
                {
                    UserId = UserId,
                    Name = userInfo.Name
                }),
                AddGroupEvent = user.LazilyEvaluate(new AddGroup
                {
                    GroupId = GroupId,
                    Name = "Unknown"
                })
            };

            return new DomainResult<Result>()
                .WithValue(result);
        }

        public class Result : IServiceResult<Result>
        {
            public Operation<User, GroupAdded> AddGroupEvent { get; init; }
            public Operation<Group, UserAdded> AddUserEvent { get; init; }

            IEnumerable<IServiceResult> IServiceResult.Operations => new IServiceResult[] { AddGroupEvent, AddUserEvent };
        }
    }
}
