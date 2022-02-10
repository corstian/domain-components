using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Aggregate<T> : IAggregate<T>
        where T : Aggregate<T>
    {
        public Task<IEnumerable<IEvent>> Evaluate(ICommand<T> command)
            => command.Evaluate((T)this);

        public Task<E> Evaluate<E>(ICommand<T, E> @command)
            where E : IEvent<T>
            => command.Evaluate((T)this);

        public Task<(E1, E2)> Evaluate<E1, E2>(ICommand<T, E1, E2> @command)
                where E1 : IEvent<T>
                where E2 : IEvent<T>
            => command.Evaluate((T)this);

        public void Apply(IEvent<T> @event) => @event.Apply((T)this);
        public void Apply(params IEvent<T>[] events) => events.ToList().ForEach(Apply);

        public TModel Apply<TModel>(IEvent<T> @event)
            where TModel : ISnapshot<T>, new()
        {
            Apply(@event);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }
        public TModel Apply<TModel>(params IEvent<T>[] events)
            where TModel : ISnapshot<T>, new()
        {
            Apply(events);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }
    }
}
