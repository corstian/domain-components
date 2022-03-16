using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class ServiceCommand
    {
        private CommitPackagesFactory _factory = new();

        public void AddressAggregate<TAggregate>(
            TAggregate aggregate, 
            Action<CommitPackageBuilder<TAggregate>> builder)
            where TAggregate : IAggregate<TAggregate>
            => _factory.AddCommitPackage(aggregate, builder);

        public void AddressService<TService>(
            TService service,
            IServiceCommand<TService> command)
            where TService : IService<TService>
            => _factory.AddServiceCommand(service, command);

        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate() => _factory.Evaluate();
    }
}
