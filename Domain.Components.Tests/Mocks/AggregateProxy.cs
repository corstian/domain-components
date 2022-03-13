using Domain.Components.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Components.Tests.Mocks
{
    internal class AggregateProxy<T> : IAggregate<T>
        where T : IAggregate<T>, new()
    {
        private T aggregate = new();

        Task IAggregate<T>.Apply(IEvent<T> @event)
            => aggregate.Apply(@event);

        Task IAggregate<T>.Apply(params IEvent<T>[] events)
            => aggregate.Apply(events);

        Task<TModel> IAggregate<T>.Apply<TModel>(IEvent<T> @event)
            => aggregate.Apply<TModel>(@event);

        Task<TModel> IAggregate<T>.Apply<TModel>(params IEvent<T>[] events)
            => aggregate.Apply<TModel>(events);

        Task<IResult<IEnumerable<IEvent<T>>>> IAggregate<T>.Evaluate(ICommand<T> command)
            => aggregate.Evaluate(command);
    }
}
