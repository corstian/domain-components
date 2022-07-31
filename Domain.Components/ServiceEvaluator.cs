using Domain.Components.Abstractions;

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
            var promise = await Stage(service);

            if (promise.IsFailed)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);

            var value = promise.Value;

            return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons)
                    .WithValue(await value.Materialize());
        }

        public async Task<IResult<IPromise<TResult>>> Stage<TResult>(IService<TResult> service) where TResult : IServiceResult<TResult>
            => await service.Stage(_serviceProvider);
    }
}
