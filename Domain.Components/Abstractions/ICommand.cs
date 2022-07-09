namespace Domain.Components.Abstractions
{
    public interface ICommand
    {

    }

    public interface ICommand<THandler> : ICommand
        where THandler : IAggregate
    {
        public IResult<IEnumerable<IEvent<THandler>>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TResult> : ICommand
        where THandler : IAggregate
        where TResult : IMarkCommandOutput<THandler>
    {
        public IResult<TResult> Evaluate(THandler handler);
    }
}
