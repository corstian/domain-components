namespace Domain.Components.Abstractions
{
    public interface IServiceResult
    {
        public IEnumerable<IServiceResult> Operations { get; }
    }

    public interface IServiceResult<T> : IServiceResult, IServicePromise<T>
        where T : IServiceResult<T>
    {
        /*
         * Due to the constraint on T ensuring it is the current instance, we can
         * safely return this object as being T. The sole thing we have to do is to 
         * ensure that the results that have been returned are actually properly evaluated.
         */
        async Task<T> IServicePromise<T>.Evaluate()
        {
            /*
             * ToDo: Evaluate all operations
             * Possible types:
             * - IServiceResult
             * - IServiceResult<T> (With IServicePromise)
             * - IStagedCommand (Which can independently be evaluated)
             * 
             * For each one must:
             * if service result: Evaluate all containing operations
             * if staged command: evaluate staged command
             * 
             * It might be beneficial if the service results are flattened first, after which they are grouped by aggregate and applied in bulk.
             */
            return (T)this;
        }
    }
}
