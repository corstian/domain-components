namespace Domain.Components.Abstractions
{
    public interface IServiceResult
    {
        public IEnumerable<IServiceResult> Operations { get; }
    }

    public interface IServiceResult<T> : IServiceResult, IPromise<T>
        where T : IServiceResult<T>
    {
        T IPromise<T>.Materialize()
        {
            return (T)this;
        }
    }
}
