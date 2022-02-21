using Domain.Components.Abstractions;
using System.Collections.Generic;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceCommand : ICommand<InterfaceAggregate, InterfaceEvent>
    {
        private DomainResult<InterfaceEvent> _evaluate => DomainResult.Ok(new InterfaceEvent());

        IResult<InterfaceEvent> ICommand<InterfaceAggregate, InterfaceEvent>.Evaluate(InterfaceAggregate handler)
            => _evaluate;

        IResult<IEnumerable<IEvent<InterfaceAggregate>>> ICommand<InterfaceAggregate>.Evaluate(InterfaceAggregate handler)
            => _evaluate.ToResult(e => new[] { e });
    }
}
