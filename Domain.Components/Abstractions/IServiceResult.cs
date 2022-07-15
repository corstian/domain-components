namespace Domain.Components.Abstractions
{
    public interface IServiceResult
    {
        public IEnumerable<IMarkCommandOutput> Results { get; }
    }
}
