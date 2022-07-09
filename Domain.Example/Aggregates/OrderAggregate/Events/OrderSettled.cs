using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.OrderAggregate.Events
{
    internal class OrderSettled : Event<Order>, IEvent<Order>
    {
        void IEvent<Order>.Apply(Order state)
        {
            state.IsSettled = true;
        }
    }
}
