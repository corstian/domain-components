namespace Domain.Components.Abstractions
{
    public interface ICommand<THandler>
        where THandler : IAggregate<THandler>
    {
        public IResult<IEnumerable<IEvent<THandler>>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, out TEvent> : ICommand<THandler>
            where THandler : IAggregate<THandler>
            where TEvent : IEvent<THandler>
    {
        public new IResult<TEvent> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent1, TEvent2> : ICommand<THandler>
        where THandler : IAggregate<THandler>
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        public IResult<(TEvent1, TEvent2)> Evaluate(THandler handler);
    }
}
