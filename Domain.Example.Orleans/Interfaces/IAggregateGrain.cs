using Domain.Components.Abstractions;
using Orleans;

namespace Domain.Example.Orleans.Interfaces
{
    public interface IAggregateGrain<T> : IGrainWithGuidKey
        where T : IAggregate<T>
    {
        public Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command);
        public Task<IResult<IEvent<T>>> Evaluate(ICommand<T, IEvent<T>> command);
        public Task<IResult<(IEvent<T>, IEvent<T>)>> Evalute(ICommand<T, IEvent<T>, IEvent<T>> command);
    }
}
