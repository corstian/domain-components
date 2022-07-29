using Domain.Components.Abstractions;
using Orleans;

namespace Domain.Example.Orleans.Interfaces
{
    public interface IServiceEvaluatorGrain : IServiceEvaluator, IGrain
    {
    }
}
