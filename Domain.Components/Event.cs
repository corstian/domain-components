﻿using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Event : IEvent
    {
        public Guid AggregateId { get; internal set; }
        public DateTime Timestamp { get; internal set; }
        public IAuthorizationContext? AuthorizationContext { get; internal set; }
    }

    public abstract class Event<T> : Event, ICommandResult<T>
        where T : IAggregate
    {
        IEnumerable<IEvent<T>> ICommandResult<T>.Result => new IEvent<T>[] { (IEvent<T>)this };
    }
}
