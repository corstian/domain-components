using Domain.Components.Abstractions;

namespace Domain.Components
{
    [Obsolete("The service should be refactored out in favour of the service command")]
    public abstract class Service<TService> : IService<TService>
        where TService : Service<TService>
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(IServiceCommand<TService> command)
            => command.Evaluate((TService)this);
    }
}
