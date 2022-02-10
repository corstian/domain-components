namespace Domain.Components.Abstractions
{
    public interface ICommandHandler<T>
        where T : ICommandHandler<T>
    {
    }
}