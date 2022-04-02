using Domain.Components.Abstractions;
using Domain.Example.Aggregates.OrderAggregate;
using Domain.Example.Aggregates.PaymentAggregate;
using Domain.Example.Aggregates.PaymentAggregate.Commands;
using Domain.Example.Aggregates.PaymentAggregate.Events;
using Domain.Example.ProcessManagers;
using Domain.Example.Tests.Mocks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Example.Tests
{
    public class OrderPaymentProcessTest
    {
        /*
         * This is a proof of concept showing what the integration of a long-running process could roughly look like.
         * When compared with what would be a real-world implementation there are still a few missing pieces. Among this
         * is a component which uses reflection to figure out all the components implementing an `IHandle` interface, retrieving
         * the reducer and then forwarding the events. The reducer allows efficient routing of events in a way that new instances
         * of long running processes can be (re)instantiated on demand because the identity of the process can be derived from the event.
         * 
         * In this specific case the process holds track of multiple events in order to collect all required information. The benefit here
         * is that information can be retrieved without having to query the domain data itself.
         */
        [Fact]
        public async Task TestPaymentProcess()
        {
            var bus = new MockEventBus();

            var orderRepo = new MockRepository<Order>();
            var paymentRepo = new MockRepository<Payment>();

            IServiceCollection collection = new ServiceCollection();
            collection.AddSingleton<IRepository<Order>>(orderRepo);
            collection.AddSingleton<IRepository<Payment>>(paymentRepo);
            var serviceProvider = collection.BuildServiceProvider();

            var process = new OrderPaymentProcess(serviceProvider);

            bus.EventStream.Subscribe(async @event =>
            {
                if (@event is PaymentInitiated _1) await process.Process(_1);
                if (@event is PaymentConfirmed _2) await process.Process(_2);
            });


            var order = await orderRepo.ById(Guid.NewGuid());
            var payment = await paymentRepo.ById(Guid.NewGuid());

            var paymentInitiated = await payment.Evaluate(new InitiatePayment
            {
                Amount = 10,
                OrderId = order.Id
            });
            await payment.Apply(paymentInitiated.Value);
            bus.Publish(paymentInitiated.Value);

            System.Threading.Thread.Sleep(1000);

            var paymentConfirmed = await payment.Evaluate(new ConfirmPayment());
            await payment.Apply(paymentConfirmed.Value);
            bus.Publish(paymentConfirmed.Value);

            Assert.Equal(order.Id, process.OrderId);
            Assert.True(order.IsSettled);
            Assert.Equal(order.Id, payment.OrderId);
            Assert.NotEqual(order.Id, payment.Id);
        }
    }
}
