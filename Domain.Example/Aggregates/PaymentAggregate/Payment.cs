using Domain.Components;
using NodaMoney;

namespace Domain.Example.Aggregates.PaymentAggregate
{
    public class Payment : Aggregate<Payment>
    {
        public Money Amount { get; internal set; }
        public byte[] Secret { get; internal set; } = Array.Empty<byte>();
        public bool IsConfirmed { get; internal set; }
        public Guid OrderId { get; internal set; }
    }
}
