using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.OrderAggregate.Events;

namespace Domain.Example.Aggregates.OrderAggregate.Commands
{
    public class SettleOrder : Command, ICommand<Order, OrderSettled>
    {
        IResult<OrderSettled> ICommand<Order, OrderSettled>.Evaluate(Order handler)
            => DomainResult.Ok(new OrderSettled());
    }
}
