using System.Linq.Expressions;

namespace Domain.Components.Abstractions
{
    public interface ISubscription<E>
        where E : IEvent
    {
        Expression<Func<E, string>> Target { get; }
    }
}
