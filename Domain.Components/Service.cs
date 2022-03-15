using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Service<TService> : IService<TService>
        where TService : Service<TService>
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(IServiceCommand<TService> command)
            => command.Evaluate((TService)this);
    }
}
