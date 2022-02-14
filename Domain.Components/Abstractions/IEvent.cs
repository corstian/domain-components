namespace Domain.Components.Abstractions
{
    public interface IEvent
    {
        Guid AggregateId { get; }
        IAuthorizationContext? AuthorizationContext { get; }
    }

    public interface IEvent<THandler> : IEvent
        where THandler : IAggregate<THandler>
    {
        public void Apply(THandler state);
    }
}
