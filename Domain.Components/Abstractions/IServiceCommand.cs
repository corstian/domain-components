namespace Domain.Components.Abstractions
{
    public interface IServiceCommand
    {

    }

    public interface IServiceCommand<TArg> : IServiceCommand
    {
        public Task<IResult<IEnumerable<ICommitPackage>>> Evaluate(TArg args);
    }

    public interface IServiceCommand<TArg, TAggregate> : IServiceCommand
            where TAggregate : IAggregate<TAggregate>
    {
        public Task<IResult<ICommitPackage<TAggregate>>> Evaluate(TArg service);
    }

    public interface IServiceCommand<TArg, TAggregate1, TAggregate2> : IServiceCommand
        where TAggregate1 : IAggregate<TAggregate1>
        where TAggregate2 : IAggregate<TAggregate2>
    {
        public Task<IResult<(
            ICommitPackage<TAggregate1>, 
            ICommitPackage<TAggregate2>)>> 
            Evaluate(TArg service);
    }
}
