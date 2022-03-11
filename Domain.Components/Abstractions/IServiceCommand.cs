namespace Domain.Components.Abstractions
{
    public interface IServiceCommand
    {

    }

    public interface IServiceCommand<THandler> : IServiceCommand
        where THandler : IService<THandler>
    {
        public Task<IResult<
            IEnumerable<ICommitPackage>>> 
            Evaluate(THandler service);
    }

    public interface IServiceCommand<THandler, TAggregate> : IServiceCommand
            where THandler : IService<THandler>
            where TAggregate : IAggregate<TAggregate>
    {
        public Task<IResult<
            ICommitPackage<TAggregate>>> 
            Evaluate(THandler service);
    }

    public interface IServiceCommand<THandler, TAggregate1, TAggregate2> : IServiceCommand
        where THandler : IService<THandler>
        where TAggregate1 : IAggregate<TAggregate1>
        where TAggregate2 : IAggregate<TAggregate2>
    {
        public Task<IResult<(
            ICommitPackage<TAggregate1>, 
            ICommitPackage<TAggregate2>)>> 
            Evaluate(THandler service);
    }
}
