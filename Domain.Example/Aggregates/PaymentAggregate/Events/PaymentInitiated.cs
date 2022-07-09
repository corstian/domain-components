using Domain.Components;
using Domain.Components.Abstractions;
using NodaMoney;

namespace Domain.Example.Aggregates.PaymentAggregate.Events
{
    public class PaymentInitiated : Event, IEvent<Payment>
    {
        internal PaymentInitiated() { }

        public Money Amount { get; init; }
        public byte[] Secret { get; init; } = Array.Empty<byte>();
        public Guid OrderId { get; init; }

        void IEvent<Payment>.Apply(Payment state)
        {
            state.Amount = Amount;
            state.Secret = Secret;
            state.OrderId = OrderId;
       }
    }
}
