using Domain.Components.Abstractions;
using Domain.Components.Extensions;

namespace Domain.Components
{
    public class BatchedServiceEvaluator : IServiceEvaluator
    {
        private readonly IServiceProvider _serviceProvider;

        public BatchedServiceEvaluator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) where TResult : IServiceResult<TResult>
        {
            var promise = await service.Stage(_serviceProvider);

            if (promise.IsFailed)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);

            var value = promise.Value;

            var materialized = promise.Value.Materialize();

            var groups = materialized.Operations.OperationsFromServiceResults().Group();

            var results = new List<IResult<ICommandResult>>();

            foreach (var group in groups)
                results.AddRange(
                    await group
                        .First()
                        .Aggregate
                        .Evaluate(group
                            .Select(q => q.Command)
                            .ToArray()));

            // ToDo: Inspect the contents of the results. If no blockers, apply all operations

            foreach (var group in groups)
            {
                await group
                    .First()
                    .Aggregate
                    .Apply(group
                        .SelectMany(q => q.Result.Events)
                        .ToArray());
            }

            return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons)
                    .WithValue(value.Materialize());
        }
    }
}
