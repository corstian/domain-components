using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.PaymentAggregate.Events;
using NodaMoney;
using System.Security.Cryptography;

namespace Domain.Example.Aggregates.PaymentAggregate.Commands
{
    public class InitiatePayment : Command, ICommand<Payment, PaymentInitiated>
    {
        public Money Amount { get; init; }
        public Guid OrderId { get; init; }

        IResult<PaymentInitiated> ICommand<Payment, PaymentInitiated>.Evaluate(Payment handler)
        {
            var secret = new byte[128];

            RandomNumberGenerator.Create().GetNonZeroBytes(secret);

            return DomainResult.Ok(new PaymentInitiated
            {
                Amount = Amount,
                OrderId = OrderId,
                Secret = secret
            });
        }
    }
}
