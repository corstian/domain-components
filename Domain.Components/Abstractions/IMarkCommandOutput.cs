namespace Domain.Components.Abstractions
{
    public interface IMarkCommandOutput { }

    /// <summary>
    /// This is merely a temporary and internal marker interface for which I should
    /// probably figure out a better name.
    /// </summary>
    public interface IMarkCommandOutput<TAggregate> : IMarkCommandOutput
        where TAggregate : IAggregate
    {
    }
}
