using FluentResults;

namespace Domain.Components.Abstractions
{
    public interface ICommand { }

    public interface ICommand<THandler> : ICommand
        where THandler : IAggregate<THandler>
    {
        public Task<Result<IEnumerable<IEvent<THandler>>>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent> : ICommand
            where THandler : IAggregate<THandler>
            where TEvent : IEvent<THandler>
    {
        public Task<Result<TEvent>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent1, TEvent2> : ICommand
        where THandler : IAggregate<THandler>
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        public Task<Result<(TEvent1, TEvent2)>> Evaluate(THandler handler);
    }
}
