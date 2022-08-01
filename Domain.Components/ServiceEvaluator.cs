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

        public async Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) where TResult : IServiceResult<TResult>
        {
            var promise = await service.Stage(_serviceProvider);

            if (promise.IsFailed)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);

            var value = promise.Value;

            var materialized = promise.Value.Materialize();

            var operations = materialized.Operations.OperationsFromServiceResults();

            var results = new List<IResult<ICommandResult>>();

            foreach (var operation in operations)
                await operation.Evaluate();

            foreach (var operation in operations)
                await operation.Aggregate.Apply(operation.Result.Events.ToArray());

            return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons)
                    .WithValue(materialized);
        }
    }
}
