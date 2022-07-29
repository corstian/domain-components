namespace Domain.Components.Abstractions
{
    public interface IServiceEvaluator
    {
        public Task<IResult<IServicePromise<TResult>>> Stage<TResult>(IService<TResult> service)
            where TResult : IServiceResult<TResult>;

        public Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service)
            where TResult : IServiceResult<TResult>;
    }
}
