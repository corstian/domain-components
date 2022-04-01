using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.OrderAggregate;
using Domain.Example.Aggregates.OrderAggregate.Commands;
using Domain.Example.Aggregates.PaymentAggregate.Events;

namespace Domain.Example.ProcessManagers
{
    public class OrderPaymentProcess : LongRunningProcess<OrderPaymentProcess>,
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
