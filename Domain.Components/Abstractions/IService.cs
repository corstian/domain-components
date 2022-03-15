namespace Domain.Components.Abstractions
{
    public interface IService { }
    public interface IService<TService> : IService
        where TService : IService<TService>
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(IServiceCommand<TService> command);
    }
}
