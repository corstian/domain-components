namespace Domain.Components.Abstractions
{
    public interface IProcess<TProcess>
        where TProcess : IProcess<TProcess>
    {
        IRepository<T> GetRepository<T>()
            where T : IAggregate;

        T GetService<T>()
            where T : IService;
    }
}
