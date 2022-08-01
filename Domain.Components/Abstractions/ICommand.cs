namespace Domain.Components.Abstractions
{
    public interface ICommand
    {
        IResult<ICommandResult> Evaluate(IAggregate aggregate);
    }

    public interface ICommand<TAggregate> : ICommand
        where TAggregate : class, IAggregate
    {
        IResult<ICommandResult<TAggregate>> Evaluate(TAggregate aggregate);

        IResult<ICommandResult> ICommand.Evaluate(IAggregate aggregate) => Evaluate(aggregate as TAggregate);
    }

    public interface ICommand<TAggregate, TResult> : ICommand<TAggregate>
        where TAggregate : class, IAggregate
        where TResult : ICommandResult<TAggregate>
    {
        IResult<TResult> Evaluate(TAggregate aggregate);

        IResult<ICommandResult<TAggregate>> ICommand<TAggregate>.Evaluate(TAggregate aggregate)
        {
            var result = Evaluate(aggregate);

            return new DomainResult<ICommandResult<TAggregate>>()
                .WithValue(result.IsSuccess ? result.Value : null)
                .WithReasons(result.Reasons);
        }
    }
}
