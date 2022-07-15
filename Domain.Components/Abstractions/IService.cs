namespace Domain.Components.Abstractions
{
    public interface IService
    {

    }
    
    public interface IService<TOutput> : IService
        where TOutput : IServiceResult
    {
        public Task<IResult<TOutput>> Invoke();
    }

    public interface IService<TInput, TOutput> : IService
        where TOutput : IServiceResult
    {
        public Task<IResult<TOutput>> Invoke(TInput arg);
    }
}
