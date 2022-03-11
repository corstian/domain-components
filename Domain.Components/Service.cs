using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Service<TService> : IService<TService>
        where TService : Service<TService>
    {
        Task<IResult<IEnumerable<ICommitPackage>>> IService<TService>.Evaluate(IServiceCommand<TService> command)
            => command.Evaluate((TService)this);
    }
}
