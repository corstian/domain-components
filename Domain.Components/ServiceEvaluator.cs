using Domain.Components.Abstractions;
using Domain.Components.Extensions;

namespace Domain.Components
{
    public class ServiceEvaluator : IServiceEvaluator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceEvaluator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) where TResult : IServiceResult
        {
            var promise = await service.Stage(_serviceProvider);

            if (promise.IsFailed)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);

            var materialized = promise.Value.Materialize();

            var operations = materialized.Operations.OperationsFromServiceResults();

            var result = new DomainResult<TResult>();

            // Once evaluation has started we'll collect all results, even if one operation had already failed.
            // This allows clients to review all errors and increases the changes of the next operation succeeding
            // without them being surprised by additional errors being thrown that had not been evaluated before.
            foreach (var operation in operations)
                result.Reasons.AddRange((await operation.Evaluate()).Reasons);

            if (result.IsSuccess)
                foreach (var operation in operations)
                    await operation.Aggregate.Apply(operation.Result.Events.ToArray());

            return result.IsSuccess
                ? result.WithValue(materialized)
                : result;
        }
    }
}
