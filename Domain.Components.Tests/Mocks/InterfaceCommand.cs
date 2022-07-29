using Domain.Components.Abstractions;
using System.Collections.Generic;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceCommand : ICommand<InterfaceAggregate, InterfaceCommand.Result>
    {
        IResult<Result> ICommand<InterfaceAggregate, InterfaceCommand.Result>.Evaluate(InterfaceAggregate handler)
            => DomainResult.Ok(
                new Result {
                    Event = new InterfaceEvent()
                });

        public class Result : ICommandResult<InterfaceAggregate>
        {
            public InterfaceEvent Event { get; init; }

            IEnumerable<IEvent<InterfaceAggregate>> ICommandResult<InterfaceAggregate>.Value => new[] { Event };
        }
    }
}
