namespace Domain.Components.Abstractions
{
    public interface IService<TService>
        where TService : IService<TService>
    {
        // Evaluation handlers
        public Task<IResult<IEnumerable<IEvent>>> Evaluate(IServiceCommand<TService> command);

        // Commit handlers without snapshot returns
        public Task Commit(params IEvent[] events);
        public Task Commit(IEvent @event);

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
