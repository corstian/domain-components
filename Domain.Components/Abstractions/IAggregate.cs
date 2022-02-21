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
        public IResult<IEnumerable<IEvent<TAggregate>>> Evaluate(ICommand<TAggregate> command);

        // Apply without snapshot return
        public void Apply(IEvent<TAggregate> @event);
        public void Apply(params IEvent<TAggregate>[] events);

        // Apply with snapshot
        public TModel Apply<TModel>(IEvent<TAggregate> @event)
            where TModel : ISnapshot<TAggregate>, new();
        public TModel Apply<TModel>(params IEvent<TAggregate>[] events)
            where TModel : ISnapshot<TAggregate>, new();
    }
}
