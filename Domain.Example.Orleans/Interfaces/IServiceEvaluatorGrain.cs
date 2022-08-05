using Domain.Components.Abstractions;
using Orleans;

namespace Domain.Example.Orleans.Interfaces
{
    public interface IServiceEvaluatorGrain : IGrainWithIntegerKey
    {
        public Task<IResult<TResult>> Evaluate<TResult>(IService<TResult> service)
            where TResult : IServiceResult;
    }
}
