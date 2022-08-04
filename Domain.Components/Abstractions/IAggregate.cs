namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        ValueTask<string> GetIdentity();
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : class, IAggregate<TAggregate>
    {
        // Command handlers
        Task<IResult<ICommandResult<TAggregate>>> Evaluate(ICommand<TAggregate> command);

        Task<IResult<TResult>> Evaluate<TResult>(ICommand<TAggregate, TResult> command)
            where TResult : ICommandResult<TAggregate>;
        //{
        //    var result = await Evaluate(command as ICommand<TAggregate>);

        //    return new DomainResult<TResult>()
        //        .WithValue(result.IsSuccess
        //            ? result.Value as TResult
        //            : null)
        //        .WithReasons(result.Reasons);
        //}

        Task<IEnumerable<IResult<ICommandResult<TAggregate>>>> Evaluate(params ICommand<TAggregate>[] commands);
        //{
        //    var results = new List<IResult<ICommandResult<TAggregate>>>();
        //    foreach (var command in commands)
        //        results.Add(await Evaluate(command));
        //    return results;
        //}

        // Event Application
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        Task Apply(ICommandResult<TAggregate> commandResult)
            => Apply(commandResult.Events.ToArray());

        // Snapshotting
        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new();
    }
}
