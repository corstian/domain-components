namespace Domain.Components.Abstractions
{
    public interface IEventHandler<T>
        where T : IEventHandler<T>
    {
    }
}
