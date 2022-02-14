namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        public Guid Id { get; }
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : IAggregate<TAggregate>
    {
        // Command handlers
        public Task<Result<IEnumerable<Event<TAggregate>>>> Evaluate(ICommand<TAggregate> command);

        public Task<Result<TEvent>> Evaluate<TEvent>(ICommand<TAggregate, TEvent> command)
            where TEvent : Event<TAggregate>;

        public Task<Result<(TEvent1, TEvent2)>> Evaluate<TEvent1, TEvent2>(ICommand<TAggregate, TEvent1, TEvent2> command)
            where TEvent1 : Event<TAggregate>
            where TEvent2 : Event<TAggregate>;

        // Apply without snapshot return
        public void Apply(Event<TAggregate> @event);
        public void Apply(params Event<TAggregate>[] events);

        // Apply with snapshot
        public TModel Apply<TModel>(Event<TAggregate> @event)
            where TModel : ISnapshot<TAggregate>, new();
        public TModel Apply<TModel>(params Event<TAggregate>[] events)
            where TModel : ISnapshot<TAggregate>, new();
    }
}
