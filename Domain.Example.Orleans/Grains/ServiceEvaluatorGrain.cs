using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans;

namespace Domain.Example.Orleans.Grains
{
    public class ServiceEvaluatorGrain : Grain, IServiceEvaluatorGrain
    {
        private readonly ServiceEvaluator _serviceEvaluator;

        public ServiceEvaluatorGrain(ServiceEvaluator serviceEvaluator)
        {
            _serviceEvaluator = serviceEvaluator;
        }

        public Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) 
            where TResult : IServiceResult<TResult>
            => _serviceEvaluator.Evaluate(service);

        public Task<IResult<IPromise<TResult>>> Stage<TResult>(IService<TResult> service)
            where TResult : IServiceResult<TResult>
            => _serviceEvaluator.Stage(service);
    }
}
