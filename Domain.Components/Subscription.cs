using Domain.Components.Abstractions;
using System.Linq.Expressions;

namespace Domain.Components
{
    public class Subscription<E> : ISubscription<E>
        where E : IEvent
    {
        public Expression<Func<E, string>> Target { get; init; }
    }
}
