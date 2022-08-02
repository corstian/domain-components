using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceAggregate : IAggregate<InterfaceAggregate>
    {
        public Guid Id { get; init; }

        public Task Apply(IEvent<InterfaceAggregate> @event)
        {
            @event.Apply(this);

            return Task.CompletedTask;
        }

        public Task Apply(params IEvent<InterfaceAggregate>[] events)
        {
            foreach (var @event in events)
                @event.Apply(this);

            return Task.CompletedTask;
        }

        public Task Apply(ICommandResult<InterfaceAggregate> commandResult)
            => Apply(commandResult.Events.ToArray());

        public Task<IResult<TResult>> Evaluate<TResult>(ICommand<InterfaceAggregate, TResult> command) 
            where TResult : ICommandResult<InterfaceAggregate>
            => Task.FromResult(command.Evaluate(this));

        public ValueTask<string> GetIdentity()
            => ValueTask.FromResult(Id.ToString());

        public Task<TModel> GetSnapshot<TModel>() where TModel : ISnapshot<InterfaceAggregate>, new()
        {
            var model = Activator.CreateInstance<TModel>();
            model.Populate(this);
            return Task.FromResult(model);
        }
    }
}
