using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans.EventSourcing;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrain<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        public Task Apply(IEvent<T> @event)
            => State.Apply(@event);

        public Task Apply(params IEvent<T>[] events)
            => State.Apply(events);

        public Task<TModel> Apply<TModel>(IEvent<T> @event) where TModel : ISnapshot<T>, new()
            => State.Apply<TModel>(@event);

        public Task<TModel> Apply<TModel>(params IEvent<T>[] events) where TModel : ISnapshot<T>, new()
            => State.Apply<TModel>(events);

        public Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
            => State.Evaluate(command);
    }
}
