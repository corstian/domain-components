using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Concurrency;

namespace Domain.Example.Orleans.Grains
{
    [StatelessWorker]
    public class ServiceEvaluatorGrain : Grain, IServiceEvaluatorGrain
    {
        private readonly IServiceEvaluator _serviceEvaluator;
        private readonly ILogger<ServiceEvaluatorGrain> _logger;

        public ServiceEvaluatorGrain(
            IServiceEvaluator serviceEvaluator,
            ILogger<ServiceEvaluatorGrain> logger)
        {
            _serviceEvaluator = serviceEvaluator;
            _logger = logger;
        }

        public async Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service) 
            where TResult : IServiceResult
        {
            _logger.LogInformation("Starting evaluation of service: {@service}", service);
            var result = await _serviceEvaluator.Evaluate(service);
            _logger.LogInformation("Finished service evaluation with result: {@result}", result);
            return result;
        }
    }
}
