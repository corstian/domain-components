using Domain.Components;
using NodaMoney;

namespace Domain.Example.Aggregates.OrderAggregate
{
    public class Order : Aggregate<Order>
    {
        public DateTime Timestamp { get; internal set; }
        public Money Total { get; internal set; }
        public bool IsSettled { get; internal set; }
    }
}
