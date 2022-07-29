namespace Domain.Components.Abstractions
{
    public interface ICommand
    {

    }

    public interface ICommand<TAggregate> : ICommand
        where TAggregate : IAggregate
    {
        public IResult<IEnumerable<IEvent<TAggregate>>> Evaluate(TAggregate handler);
    }

    public interface ICommand<TAggregate, TResult> : ICommand
        where TAggregate : IAggregate
        where TResult : ICommandResult<TAggregate>
    {
        public IResult<TResult> Evaluate(TAggregate aggregate);
    }
}
