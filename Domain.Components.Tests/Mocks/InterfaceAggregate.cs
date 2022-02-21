using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Components.Tests.Mocks
{
    public class InterfaceAggregate : IAggregate<InterfaceAggregate>
    {
        public Guid Id { get; init; }

        public void Apply(IEvent<InterfaceAggregate> @event)
            => @event.Apply(this);

        public void Apply(params IEvent<InterfaceAggregate>[] events)
            => events.ToList().ForEach(@event => @event.Apply(this));

        public TModel Apply<TModel>(IEvent<InterfaceAggregate> @event) where TModel : ISnapshot<InterfaceAggregate>, new()
        {
            @event.Apply(this);
            return _createSnapshot<TModel>();
        }

        public TModel Apply<TModel>(params IEvent<InterfaceAggregate>[] events) where TModel : ISnapshot<InterfaceAggregate>, new()
        {
            events.ToList().ForEach(@event => @event.Apply(this));
            return _createSnapshot<TModel>();
        }

        public IResult<IEnumerable<IEvent<InterfaceAggregate>>> Evaluate(ICommand<InterfaceAggregate> command)
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
