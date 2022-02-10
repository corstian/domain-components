namespace Domain.Components.Abstractions
{
    public interface ICommand { }

    public interface ICommand<THandler> : ICommand
        where THandler : ICommandHandler<THandler>
    {
        public Task<IEnumerable<IEvent>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent> : ICommand
            where THandler : ICommandHandler<THandler>
            where TEvent : IEvent
    {
        public Task<TEvent> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent1, TEvent2> : ICommand
        where THandler : ICommandHandler<THandler>
        where TEvent1 : IEvent
        where TEvent2 : IEvent
    {
        public Task<(TEvent1, TEvent2)> Evaluate(THandler handler);
    }
}
