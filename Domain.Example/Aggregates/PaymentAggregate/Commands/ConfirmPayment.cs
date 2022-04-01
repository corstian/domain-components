using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.PaymentAggregate.Events;

namespace Domain.Example.Aggregates.PaymentAggregate.Commands
{
    public class ConfirmPayment : Command, ICommand<Payment, PaymentConfirmed>
    {
        public IResult<PaymentConfirmed> Evaluate(Payment handler)
            => DomainResult.Ok(new PaymentConfirmed());
    }
}
