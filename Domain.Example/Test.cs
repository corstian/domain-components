using Domain.Components.Abstractions;
using Domain.Components.Experiment;
using Domain.Components.Extensions;
using Domain.Example.Aggregates.GroupAggregate;
using Domain.Example.Aggregates.GroupAggregate.Commands;
using Domain.Example.Aggregates.UserAggregate;
using Domain.Example.Aggregates.UserAggregate.Commands;

namespace Domain.Example
{
    internal class Test : Components.Experiment.IService<
        CommandPackage<User, AddGroup>,
        CommandPackage<Group, AddUser>>
    {
        private readonly IRepository<Group> _groupRepo;
        private readonly IRepository<User> _userRepo;


        public Test(
            IRepository<Group> groupRepo,
            IRepository<User> userRepo)
        {
            _groupRepo = groupRepo;
            _userRepo = userRepo;
        }

        public async Task<IServiceResult<CommandPackage<User, AddGroup>, CommandPackage<Group, AddUser>>> Evaluate()
        {
            var user = await _userRepo.ById(Guid.Empty);
            var group = await _groupRepo.ById(Guid.Empty);

            var result = ServiceResultBuilder
                .AddCommandPackage(builder => builder
                    .Target(user)
                    .WithCommand(new AddGroup
                    {
                        GroupId = group.Id,
                        Name = group.Name
                    })
                )
                .AddCommandPackage(builder => builder
                    .Target(group)
                    .WithCommand(new AddUser
                    {
                        UserId = user.Id,
                        Name = user.Name
                    }));

            return result;
        }
    }

    internal class Test2 : Components.Experiment.IService<
        CommandPackage<User, AddGroup>,
        CommandPackage<Group, AddUser>,
        CommandPackage<User, Rename>>
    {
        private readonly Test _testService;
        private readonly IRepository<User> _userRepo;

        public Test2(
            Test testService,
            IRepository<User> userRepo)
        {
            _testService = testService;
            _userRepo = userRepo;
        }

        public async Task<IServiceResult<
            CommandPackage<User, AddGroup>, 
            CommandPackage<Group, AddUser>, 
            CommandPackage<User, Rename>>> Evaluate()
        {
            var user = await _userRepo.ById(Guid.Empty);


            // How we can add additional commands
            // _testService.Evaluate().Result.CommandPackage1.WithCommand(new Rename { Name = "" });

            var result = ServiceResultBuilder
                .AddService(() => _testService.Evaluate())
                .AddCommandPackage(builder => builder
                    .Target(user)
                    .WithCommand(new Rename
                    {
                        Name = "John Doe"
                    }));

            return result;
        }
    }
}
