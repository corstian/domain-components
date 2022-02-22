using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans.EventSourcing;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrain<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        public Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
        {
            var result = State.Evaluate(command);

            if (result.IsSuccess)
            {
                RaiseEvents(result.Value);
            }

            return Task.FromResult(result);
        }

        public Task<IResult<IEvent<T>>> Evaluate(ICommand<T, IEvent<T>> command)
            => Task.FromResult(State.EvaluateTypedCommand(command));

        public Task<IResult<(IEvent<T>, IEvent<T>)>> Evalute(ICommand<T, IEvent<T>, IEvent<T>> command)
            => Task.FromResult(State.EvaluateTypedCommand(command));
    }
}
