using Domain.Components.Abstractions;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceCommand : ICommand<InterfaceAggregate, InterfaceEvent>
    {
        IResult<InterfaceEvent> ICommand<InterfaceAggregate, InterfaceEvent>.Evaluate(InterfaceAggregate handler)
            => DomainResult.Ok(new InterfaceEvent());
    }
}
