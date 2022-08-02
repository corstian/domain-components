using Domain.Components.Abstractions;
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

        public Task<IResult<TResult>> Evaluate<TResult>(ICommand<T, TResult> command)
            where TResult : ICommandResult<T>
            => aggregate.Evaluate(command);

        public Task Apply(ICommandResult<T> commandResult)
            => aggregate.Apply(commandResult);

        public ValueTask<string> GetIdentity()
            => aggregate.GetIdentity();
    }
}
