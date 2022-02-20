using Domain.Components.Abstractions;
using Orleans;

namespace Domain.Example.Orleans.Interfaces
{
    public interface IAggregateGrain<T> : IGrainWithGuidKey
        where T : IAggregate<T>
    {
        public Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command);
    }
}
