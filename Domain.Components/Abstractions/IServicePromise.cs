namespace Domain.Components.Abstractions
{
    public interface IPromise<T>
        where T : IServiceResult
    {
        internal Task<T> Materialize();
    }
}
