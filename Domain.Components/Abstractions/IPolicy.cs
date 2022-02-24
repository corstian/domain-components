namespace Domain.Components.Abstractions
{
    public interface IPolicy<E>
    {
        Task Respond(E @event);
    }
}
