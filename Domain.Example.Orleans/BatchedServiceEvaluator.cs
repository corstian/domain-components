using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Components.Extensions;

namespace Domain.Example.Orleans
{
    public class BatchedServiceEvaluator : IServiceEvaluator
    {
        private readonly IServiceProvider _serviceProvider;

        public BatchedServiceEvaluator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) where TResult : IServiceResult
        {
            throw new NotImplementedException();

            var promise = await service.Stage(_serviceProvider);

            if (promise.IsFailed)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);

            var materialized = ServicePromise.Materialize(promise.Value);

            var groups = materialized.Operations
                .OperationsFromServiceResults()
                .Select(async q => new
                {
                    Operation = q,
                    AggregateIdentity = await q.Aggregate.GetIdentity()
                })
                .Select(q => q.Result)
                .GroupBy(q => q.AggregateIdentity, (key, group) => group.Select(w => w.Operation));


            var result = new DomainResult<TResult>();

            foreach (var group in groups)
            {
                var batchEvaluationResult = await group
                    .First()
                    .Aggregate
                    .Evaluate(group
                        .Select(q => q.Command)
                        .ToArray());

                foreach (var evaluationResult in batchEvaluationResult)
                {
                    // Signalling completion back to the operation
                    foreach (var operation in group)
                    {
                        operation.SignalEvaluation(evaluationResult);
                    }
                }

                result.Reasons.AddRange(batchEvaluationResult.SelectMany(q => q.Reasons));
            }

            if (result.IsSuccess)
                foreach (var group in groups)
                    await group
                        .First()
                        .Aggregate
                        .Apply(group
                            .SelectMany(q => q.Result.Events)
                            .ToArray());

            return result.IsSuccess
                ? result.WithValue(materialized)
                : result;
        }
    }
}
