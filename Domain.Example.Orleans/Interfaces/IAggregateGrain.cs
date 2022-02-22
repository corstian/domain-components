using Domain.Components.Abstractions;
using Orleans;

namespace Domain.Example.Orleans.Interfaces
{
    public interface IAggregateGrain<T> : IAggregate<T>, IGrainWithGuidKey
        where T : IAggregate<T>
    {
    }
}
