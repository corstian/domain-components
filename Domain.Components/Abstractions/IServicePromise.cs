namespace Domain.Components.Abstractions
{
    public interface IServicePromise<T>
        where T : IServiceResult
    {
        public Task<T> Evaluate();
    }
}
