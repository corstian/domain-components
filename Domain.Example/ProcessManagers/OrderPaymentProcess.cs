using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.OrderAggregate;
using Domain.Example.Aggregates.OrderAggregate.Commands;
using Domain.Example.Aggregates.PaymentAggregate.Events;

namespace Domain.Example.ProcessManagers
{
    /*
     * Currently a limitation here is that this process might claim to handle multiple events while
     * providing an identity type (through the reducer) that is incompatible. Ultimately we'd have
     * a compile time check to see whether the identity reducers provide the correct type for this class.
     * 
     * At the time of writing I have no idea about how I can, at the same time:
     * 1. Provide compile time information about the handled events
     * 2. Provide compile time information about how to derive the identity of the process from these events
     * 3. Provide a compile time check for the type equality of the provided identity reducers
     */
    public class OrderPaymentProcess : Process<OrderPaymentProcess>,
        IHandle<PaymentInitiated, AggregateIdEventReducer>,
        IHandle<PaymentConfirmed, AggregateIdEventReducer>
    {
        public OrderPaymentProcess(IServiceProvider serviceProvider) : base(serviceProvider) { }
        
        public Guid OrderId { get; set; }

        public Task Process(PaymentInitiated @event)
        {
            OrderId = @event.OrderId;

            return Task.CompletedTask;
        }

        public async Task Process(PaymentConfirmed @event)
        {
            var order = await GetRepository<Order>().ById(OrderId);

            var result = await order.Evaluate(new SettleOrder());
            await order.Apply(result.Value);
        }
    }
}
