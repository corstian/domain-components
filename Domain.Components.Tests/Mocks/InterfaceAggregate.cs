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

        public async Task Apply(IEvent<InterfaceAggregate> @event)
        {
            @event.Apply(this);
        }

        public async Task Apply(params IEvent<InterfaceAggregate>[] events)
        {
            foreach (var @event in events)
                @event.Apply(this);
        }

        public async Task<TModel> Apply<TModel>(IEvent<InterfaceAggregate> @event) where TModel : ISnapshot<InterfaceAggregate>, new()
        {
            @event.Apply(this);
            return _createSnapshot<TModel>();
        }

        public async Task<TModel> Apply<TModel>(params IEvent<InterfaceAggregate>[] events) where TModel : ISnapshot<InterfaceAggregate>, new()
        {
            events.ToList().ForEach(@event => @event.Apply(this));
            return _createSnapshot<TModel>();
        }

        public async Task<IResult<IEnumerable<IEvent<InterfaceAggregate>>>> Evaluate(ICommand<InterfaceAggregate> command)
            => command.Evaluate(this);

        private TSnapshot _createSnapshot<TSnapshot>()
            where TSnapshot : ISnapshot<InterfaceAggregate>
        {
            var model = Activator.CreateInstance<TSnapshot>();
            model.Populate(this);
            return model;
        }
    }
}
