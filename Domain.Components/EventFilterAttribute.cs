using Domain.Components.Abstractions;

namespace Domain.Components
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class WithFilter<TFilter> : Attribute
        where TFilter : IEventFilter
    {
        public WithFilter()
        {
            
        }
    }

    public class ActivateByIdentity<TIdentity, TReducer> : Attribute
        where TReducer : IEventReducer<IEvent, TIdentity>
        where TIdentity : struct
    {

    }
}
