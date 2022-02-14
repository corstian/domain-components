using Domain.Components.Abstractions;
using FluentResults;

namespace Domain.Components
{
    public abstract class Aggregate<T> : IAggregate<T>
        where T : Aggregate<T>
    {
        public Guid Id { get; }

        public async Task<Result<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
        {
            var result = await command.Evaluate((T)this);

            if (result.IsFailed) return result;

            var events = result.Value;

            foreach (var @event in events)
            {
                if (@event is Event e)
                    e.AggregateId = Id;
            }

            return result;
        }

        public async Task<Result<E>> Evaluate<E>(ICommand<T, E> @command)
            where E : IEvent<T>
        {
            var result = await command.Evaluate((T)this);

            if (result.IsFailed) return result;

            if (result.Value is Event e)
                e.AggregateId = Id;

            return result;
        }

        public async Task<Result<(E1, E2)>> Evaluate<E1, E2>(ICommand<T, E1, E2> @command)
                where E1 : IEvent<T>
                where E2 : IEvent<T>
        {
            var result = await command.Evaluate((T)this);

            if (result.IsFailed) return result;

            if (result.Value.Item1 is Event e1)
                e1.AggregateId = Id;

            if (result.Value.Item2 is Event e2)
                e2.AggregateId = Id;
            
            return result;
        }

        public void Apply(IEvent<T> @event)
            => @event.Apply((T)this);

        public void Apply(params IEvent<T>[] events) 
            => events.ToList().ForEach(Apply);

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
