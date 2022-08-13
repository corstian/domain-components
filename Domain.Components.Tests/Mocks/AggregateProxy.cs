using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Components.Tests.Mocks
{
    internal class AggregateProxy<T> : IAggregate<T>
        where T : class, IAggregate<T>, new()
    {
        public AggregateProxy() { }

        public AggregateProxy(T aggregate)
        {
            this.aggregate = aggregate;
        }

        private T aggregate = new();

        Task IAggregate<T>.Apply(IEvent<T> @event)
            => aggregate.Apply(@event);

        Task IAggregate<T>.Apply(params IEvent<T>[] events)
            => aggregate.Apply(events);

        Task<TModel> IAggregate<T>.GetSnapshot<TModel>()
            => aggregate.GetSnapshot<TModel>();

        public Task<IResult<ICommandResult<T>>> Evaluate(ICommand<T> command)
            => aggregate.Evaluate(command);

        public Task Apply(ICommandResult<T> commandResult)
            => aggregate.Apply(commandResult);

        public async Task<IEnumerable<IResult<ICommandResult<T>>>> Evaluate(params ICommand<T>[] commands)
        {
            var results = new List<IResult<ICommandResult<T>>>();
            foreach (var command in commands)
                results.Add(await Evaluate(command));
            return results;
        }
    }
}
