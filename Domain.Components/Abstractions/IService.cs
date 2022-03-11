namespace Domain.Components.Abstractions
{
    public interface IService<TService>
        where TService : IService<TService>
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(IServiceCommand<TService> command);
    }
}
