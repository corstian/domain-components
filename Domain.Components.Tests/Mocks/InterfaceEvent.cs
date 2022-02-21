using Domain.Components.Abstractions;
using System;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceEvent : IEvent<InterfaceAggregate>
    {
        public Guid AggregateId { get; init; }

        void IEvent<InterfaceAggregate>.Apply(InterfaceAggregate state)
        {
            
        }
    }
}
