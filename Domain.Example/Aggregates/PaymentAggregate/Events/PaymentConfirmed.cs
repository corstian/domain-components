using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.PaymentAggregate.Events
{
    public class PaymentConfirmed : Event, IEvent<Payment>
    {
        public void Apply(Payment state)
        {
            state.IsConfirmed = true;
        }
    }
}
