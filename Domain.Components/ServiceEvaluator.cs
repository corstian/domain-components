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

            if (promise.IsSuccess)
                return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons)
                    .WithValue(await promise.Value.Evaluate());
            else return new DomainResult<TResult>()
                    .WithReasons(promise.Reasons);
        }

        public async Task<IResult<IServicePromise<TResult>>> Stage<TResult>(IService<TResult> service) where TResult : IServiceResult<TResult>
            => await service.Stage(_serviceProvider);
    }
}
