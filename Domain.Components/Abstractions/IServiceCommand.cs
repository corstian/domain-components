using FluentResults;

namespace Domain.Components.Abstractions
{
    public interface IServiceCommand { }

    public interface IServiceCommand<THandler> : IServiceCommand
        where THandler : IService<THandler>
    {
        public Task<Result<IEnumerable<IEvent>>> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent> : IServiceCommand
            where THandler : IService<THandler>
            where TEvent : IEvent
    {
        public Task<Result<TEvent>> Evaluate(THandler handler);
    }

    public interface IServiceCommand<THandler, TEvent1, TEvent2> : IServiceCommand
        where THandler : IService<THandler>
        where TEvent1 : IEvent
        where TEvent2 : IEvent
    {
        public Task<Result<(TEvent1, TEvent2)>> Evaluate(THandler handler);
    }
}
