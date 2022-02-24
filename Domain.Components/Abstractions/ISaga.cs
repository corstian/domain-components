namespace Domain.Components.Abstractions
{
    public interface ISaga<TSaga>
        where TSaga : ISaga<TSaga>
    {
        IEnumerable<ISubscription<IEvent>> GetSubscriptions();
        void RunCorrectiveAction(IEvent @event);
    }
}
