namespace Domain.Components.Abstractions
{
    /// <summary>
    /// This is merely a temporary and internal marker interface for which I should
    /// probably figure out a better name.
    /// </summary>
    public interface IMarkCommandOutput<TAggregate>
        where TAggregate : IAggregate
    {
    }
}
