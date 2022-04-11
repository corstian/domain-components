namespace Domain.Components.Abstractions
{
    public interface IService
    {

    }

    public interface IService<TArg> : IService
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(TArg args);
    }

    public interface IService<TArg, TAggregate> : IService
            where TAggregate : IAggregate<TAggregate>
    {
        public Task<IResult<ICommitPackage<TAggregate>>> Evaluate(TArg service);
    }

    public interface IService<TArg, TAggregate1, TAggregate2> : IService
        where TAggregate1 : IAggregate<TAggregate1>
        where TAggregate2 : IAggregate<TAggregate2>
    {
        public Task<IResult<(
            ICommitPackage<TAggregate1>, 
            ICommitPackage<TAggregate2>)>> 
            Evaluate(TArg service);
    }
}
