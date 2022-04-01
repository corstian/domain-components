using Domain.Components.Abstractions;

namespace Domain.Components
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class Activator<TIdentity, TReducer> : Attribute
        where TReducer : IEventReducer<IEvent, TIdentity>
        where TIdentity : struct
    {

    }
}
