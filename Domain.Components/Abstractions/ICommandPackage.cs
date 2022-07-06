using Domain.Components.Abstractions;

namespace Domain.Components.Experiment
{
    public interface ICommandPackage  : IComposable { 
        public IAggregate Target { get; }
        public IEnumerable<ICommand> Commands { get; }
    }

    public interface ICommandPackage<TAggregate> : ICommandPackage
        where TAggregate : IAggregate
    {
        public new TAggregate Target { get; }
        public new IEnumerable<ICommand<TAggregate>> Commands { get; }

        IAggregate ICommandPackage.Target => Target;
        IEnumerable<ICommand> ICommandPackage.Commands => Commands;
    }

    public interface ICommandPackage<TAggregate, TCommand> : ICommandPackage
        where TAggregate : IAggregate
        where TCommand : ICommand<TAggregate>
    {
        public new TAggregate Target { get; }
        public TCommand Command { get; }

        IAggregate ICommandPackage.Target => Target;
        IEnumerable<ICommand> ICommandPackage.Commands => new ICommand[] { Command };
    }

    public interface ICommandPackage<TAggregate, TCommand1, TCommand2> : ICommandPackage
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
    {
        public new TAggregate Target { get; }
        public TCommand1 Command1 { get; }
        public TCommand2 Command2 { get; }

        IAggregate ICommandPackage.Target => Target;
        IEnumerable<ICommand> ICommandPackage.Commands => new ICommand[] { Command1, Command2 };
    }

    public interface ICommandPackage<TAggregate, TCommand1, TCommand2, TCommand3> : ICommandPackage
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
        where TCommand3 : ICommand<TAggregate>
    {
        public new TAggregate Target { get; }
        public TCommand1 Command1 { get; }
        public TCommand2 Command2 { get; }
        public TCommand3 Command3 { get; }

        IAggregate ICommandPackage.Target => Target;
        IEnumerable<ICommand> ICommandPackage.Commands => new ICommand[] { Command1, Command2, Command3 };
    }

    public interface ICommandPackage<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4> : ICommandPackage
        where TAggregate : IAggregate
        where TCommand1 : ICommand<TAggregate>
        where TCommand2 : ICommand<TAggregate>
        where TCommand3 : ICommand<TAggregate>
        where TCommand4 : ICommand<TAggregate>
    {
        public new TAggregate Target { get; }
        public TCommand1 Command1 { get; }
        public TCommand2 Command2 { get; }
        public TCommand3 Command3 { get; }
        public TCommand4 Command4 { get; }

        IAggregate ICommandPackage.Target => Target;
        IEnumerable<ICommand> ICommandPackage.Commands => new ICommand[] { Command1, Command2, Command3, Command4 };
    }
}
