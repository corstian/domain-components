namespace Domain.Components.Abstractions
{
    public interface ILongRunningProcess<TProcess>
        where TProcess : ILongRunningProcess<TProcess>
    {
        IRepository<T> GetRepository<T>()
            where T : IAggregate;

        IService<T> GetService<T>()
            where T : IService<T>;
    }
}
