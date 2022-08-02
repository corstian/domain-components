using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Aggregate
    {
        public Guid Id { get; init; }
    }

    public abstract class Aggregate<TAggregate> : Aggregate, IAggregate<TAggregate>
        where TAggregate : Aggregate<TAggregate>
    {
        public Task<IResult<TResult>> Evaluate<TResult>(ICommand<TAggregate, TResult> @command)
            where TResult : ICommandResult<TAggregate>
        {
            var result = command.Evaluate((TAggregate)this);

            if (result.IsFailed) return Task.FromResult(result);

            ICommandResult<TAggregate> commandResult= result.Value;

            foreach (var @event in commandResult.Events)
            {
                if (@event is Event e)
                {
                    e.AggregateId = Id;
                    e.Timestamp = DateTime.UtcNow;
                }
            }

            return Task.FromResult(result);
        }

        public Task Apply(IEvent<TAggregate> @event)
        {
            @event.Apply((TAggregate)this);

            return Task.FromResult(@event);
        }

        public async Task Apply(params IEvent<TAggregate>[] events)
        {
            foreach (var @event in events)
                await Apply(@event);
        }

        public Task Apply(ICommandResult<TAggregate> commandResult)
            => Apply(commandResult.Events.ToArray());

        public Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new()
        {
            var model = Activator.CreateInstance<TModel>();
            model.Populate((TAggregate)this);
            return Task.FromResult(model);
        }

        public ValueTask<string> GetIdentity()
            => ValueTask.FromResult(Id.ToString());
    }
}
