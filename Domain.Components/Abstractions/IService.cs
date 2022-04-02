namespace Domain.Components.Abstractions
{
    [Obsolete("IService should be merged with IServiceCommand")]
    public interface IService { }
    [Obsolete("IService should be merged with IServiceCommand")]
    public interface IService<TService> : IService
        where TService : IService<TService>
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(IServiceCommand<TService> command);
    }
}
