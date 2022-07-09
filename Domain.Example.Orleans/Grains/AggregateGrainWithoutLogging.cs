using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans.EventSourcing;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrainWithoutLogging<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        public async Task Apply(IEvent<T> @event)
            => RaiseEvent(@event);

        public async Task Apply(params IEvent<T>[] events)
            => RaiseEvents(events);

        public Task Apply(ICommandResult<T> commandResult)
            => Apply(commandResult.Result.ToArray());

        public async Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
            => await State.Evaluate(command);

        public async Task<IResult<TResult>> Evaluate<TResult>(ICommand<T, TResult> command) 
            where TResult : ICommandResult<T>
            => await State.Evaluate(command);

        public async Task<TModel> GetSnapshot<TModel>() where TModel : ISnapshot<T>, new()
            => await State.GetSnapshot<TModel>();
    }
}
