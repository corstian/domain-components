﻿namespace Domain.Components.Abstractions
{
    public interface IService<TService> : ICommandHandler<TService>
        where TService : IService<TService>
    {
        // Evaluation handlers
        public Task<IEnumerable<IEvent>> Evaluate(ICommand<TService> command);

        public Task<TEvent> Evaluate<TEvent>(ICommand<TService, TEvent> command)
            where TEvent : IEvent;

        public Task<(TEvent1, TEvent2)> Evaluate<TEvent1, TEvent2>(ICommand<TService, TEvent1, TEvent2> command)
            where TEvent1 : IEvent
            where TEvent2 : IEvent;


        // Commit handlers without snapshot returns
        public Task Commit(params IEvent[] events);
        public Task Commit(IEvent @event);
        public Task Commit((IEvent, IEvent) events);

        // Commit handlers with snapshot returns
        public Task<S> Commit<T, S>(IEvent<T> @event)
            where T : IAggregate<T>
            where S : ISnapshot<T>, new();

        public Task<(S1, S2)> Commit<T1, T2, S1, S2>((IEvent<T1>, IEvent<T2>) events)
            where T1 : IAggregate<T1>
            where T2 : IAggregate<T2>
            where S1 : ISnapshot<T1>, new()
            where S2 : ISnapshot<T2>, new();
    }
}
