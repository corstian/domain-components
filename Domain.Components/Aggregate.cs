using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Aggregate<T> : IAggregate<T>
        where T : Aggregate<T>
    {
        public Guid Id { get; }

        public async Task<Result<IEnumerable<Event<T>>>> Evaluate(ICommand<T> command)
        {
            var events = await command.Evaluate((T)this);

            foreach (var @event in events)
            {
                @event.AggregateId = Id;
            }

            return events;
        }

        public async Task<Result<E>> Evaluate<E>(ICommand<T, E> @command)
            where E : Event<T>
        {
            var @event = await command.Evaluate((T)this);

            @event.AggregateId = Id;

            return @event;
        }

        public async Task<Result<(E1, E2)>> Evaluate<E1, E2>(ICommand<T, E1, E2> @command)
                where E1 : Event<T>
                where E2 : Event<T>
        {
            var result = await command.Evaluate((T)this);

            result.Value.Item1.AggregateId = Id;

            
            events.Item1.AggregateId = Id;
            events.Item2.AggregateId = Id;

            return events;
        }

        public void Apply(Event<T> @event)
            => @event.Apply((T)this);

        public void Apply(params Event<T>[] events) 
            => events.ToList().ForEach(Apply);

        public TModel Apply<TModel>(Event<T> @event)
            where TModel : ISnapshot<T>, new()
        {
            Apply(@event);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }
        public TModel Apply<TModel>(params Event<T>[] events)
            where TModel : ISnapshot<T>, new()
        {
            Apply(events);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }
    }
}
