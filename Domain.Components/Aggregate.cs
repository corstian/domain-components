using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Aggregate<T> : IAggregate<T>
        where T : Aggregate<T>
    {
        public Guid Id { get; init; }


        public Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
        {
            var result = command.Evaluate((T)this);

            if (result.IsFailed) return Task.FromResult(result);

            var events = result.Value;

            foreach (var @event in events)
            {
                if (@event is Event e)
                    e.AggregateId = Id;
            }

            return Task.FromResult(result);
        }

        public Task<IResult<E>> Evaluate<E>(ICommand<T, E> @command)
            where E : IEvent<T>
        {
            var result = command.Evaluate((T)this);

            if (result.IsFailed) return Task.FromResult(result);

            var @event = result.Value;

            if (@event is Event e)
                e.AggregateId = Id;

            return Task.FromResult(result);
        }

        public Task<IResult<(E1, E2)>> Evaluate<E1, E2>(ICommand<T, E1, E2> @command)
                where E1 : IEvent<T>
                where E2 : IEvent<T>
        {
            var result = command.Evaluate((T)this);

            if (result.IsFailed) return Task.FromResult(result);

            if (result.Value.Item1 is Event e1)
                e1.AggregateId = Id;

            if (result.Value.Item2 is Event e2)
                e2.AggregateId = Id;
            
            return Task.FromResult(result);
        }

        public Task Apply(IEvent<T> @event)
        {
            @event.Apply((T)this);
            return Task.CompletedTask;
        }

        public async Task Apply(params IEvent<T>[] events)
        {
            foreach (var @event in events)
                await Apply(@event);
        }

        public async Task Apply(IResult<IEvent<T>> result)
        {
            if (result.IsSuccess)
                await Apply(result.Value);
        }

        public async Task<TModel> Apply<TModel>(IEvent<T> @event)
            where TModel : ISnapshot<T>, new()
        {
            await Apply(@event);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }

        public async Task<TModel> Apply<TModel>(params IEvent<T>[] events)
            where TModel : ISnapshot<T>, new()
        {
            await Apply(events);

            var model = Activator.CreateInstance<TModel>();
            model.Populate((T)this);
            return model;
        }
    }
}
