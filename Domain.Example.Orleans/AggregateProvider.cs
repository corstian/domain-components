using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans;

namespace Domain.Example.Orleans
{
    public class AggregateProvider : IAggregateProvider
    {
        private readonly IGrainFactory _grainFactory;

        public AggregateProvider(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        IAggregate<TAggregate> IAggregateProvider.Get<TAggregate>(Guid id)
            => _grainFactory.GetGrain<IAggregateGrain<TAggregate>>(id);
    }
}
