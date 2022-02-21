namespace Domain.Components.Abstractions
{
    public interface IServiceCommand<THandler>
        where THandler : IService<THandler>
    {
        public Task<IResult<IEnumerable<IEvent>>> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent> : IServiceCommand<THandler>
            where THandler : IService<THandler>
            where TEvent : IEvent
    {
        public new Task<IResult<TEvent>> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent1, TEvent2> : IServiceCommand<THandler>
        where THandler : IService<THandler>
        where TEvent1 : IEvent
        where TEvent2 : IEvent
    {
        public new Task<IResult<(TEvent1, TEvent2)>> Evaluate(THandler handler);
    }
}
