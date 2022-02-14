namespace Domain.Components.Abstractions
{
    public interface IServiceCommand { }

    public interface IServiceCommand<THandler> : IServiceCommand
        where THandler : IService<THandler>
    {
        public Task<IEnumerable<IEvent>> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent> : IServiceCommand
            where THandler : IService<THandler>
            where TEvent : Event
    {
        public Task<TEvent> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent1, TEvent2> : IServiceCommand
        where THandler : IService<THandler>
        where TEvent1 : Event
        where TEvent2 : Event
    {
        public Task<(TEvent1, TEvent2)> Evaluate(THandler handler);
    }
}
