using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class CommandPackage : ICommandPackage
    {
        public IAggregate Target { get; init; }
        public IEnumerable<ICommand> Commands { get; init; }
    }

    public class CommandPackage<TAggregate> : ICommandPackage<TAggregate>
        where TAggregate : IAggregate
    {
        public TAggregate Target { get; init; }
        public IEnumerable<ICommand<TAggregate>> Commands { get; init; }
    }

    public class CommandPackage<TAggregate, TCommand> : ICommandPackage<TAggregate, TCommand>
        where TAggregate : IAggregate
        where TCommand : ICommand<TAggregate>
    {
        public TAggregate Target { get; init; }
        public TCommand Command { get; init; }
    }

    public class CommandPackage<TAggregate, TCommand1, TCommand2> : ICommandPackage<TAggregate, TCommand1, TCommand2>
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
    {
        public TAggregate Target { get; init; }
        public TCommand1 Command1 { get; init; }
        public TCommand2 Command2 { get; init; }
    }

    public class CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3> : ICommandPackage<TAggregate, TCommand1, TCommand2, TCommand3>
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
        where TCommand3 : ICommand<TAggregate>
    {
        public TAggregate Target { get; init; }
        public TCommand1 Command1 { get; init; }
        public TCommand2 Command2 { get; init; }
        public TCommand3 Command3 { get; init; }
    }

    public class CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4> : ICommandPackage<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4>
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
        where TCommand3 : ICommand<TAggregate>
        where TCommand4 : ICommand<TAggregate>
    {
        public TAggregate Target { get; init; }
        public TCommand1 Command1 { get; init; }
        public TCommand2 Command2 { get; init; }
        public TCommand3 Command3 { get; init; }
        public TCommand4 Command4 { get; init; }
    }
}
