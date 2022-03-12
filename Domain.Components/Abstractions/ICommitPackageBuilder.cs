namespace Domain.Components.Abstractions
{
    public interface ICommitPackageBuilder
    {
        public IAggregate Aggregate { get; }
        public IList<ICommand> Commands { get; }
    }

    public interface ICommitPackageBuilder<TAggregate> : ICommitPackageBuilder
        where TAggregate : IAggregate<TAggregate>
    {
        IAggregate ICommitPackageBuilder.Aggregate => Aggregate;
        IList<ICommand> ICommitPackageBuilder.Commands => Commands.Cast<ICommand>().ToList();

        public new TAggregate Aggregate { get; }
        public new IList<ICommand<TAggregate>> Commands { get; }
    }
}
