namespace Domain.Components.Abstractions
{
    public interface IServiceEvaluator
    {
        /*
         * The TResult type was supposed to be of type `IServiceResult<TResult>`, though
         * this proved to be problematic in use with Orleans, so I dropped it for now.
         */
        public Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service)
            where TResult : IServiceResult;
    }
}
