using FluentResults;

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

        public IResult<TEvent> Evaluate<TEvent>(ICommand<TAggregate, TEvent> command)
            where TEvent : IEvent<TAggregate>;

        public IResult<(TEvent1, TEvent2)> Evaluate<TEvent1, TEvent2>(ICommand<TAggregate, TEvent1, TEvent2> command)
            where TEvent1 : IEvent<TAggregate>
            where TEvent2 : IEvent<TAggregate>;

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
