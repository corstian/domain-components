using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Orleans.EventSourcing;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrainWithoutLogging<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        public async Task Apply(IEvent<T> @event)
            => RaiseEvent(@event);

        public async Task Apply(params IEvent<T>[] events)
            => RaiseEvents(events);

        public Task Apply(ICommandResult<T> commandResult)
            => Apply(commandResult.Events.ToArray());

        public async Task<IResult<ICommandResult<T>>> Evaluate(ICommand<T> command) 
            => await State.Evaluate(command);

        public ValueTask<string> GetIdentity()
            => State.GetIdentity();

        public async Task<TModel> GetSnapshot<TModel>() where TModel : ISnapshot<T>, new()
            => await State.GetSnapshot<TModel>();

        public async Task<IResult<TResult>> Evaluate<TResult>(ICommand<T, TResult> command)
            where TResult : ICommandResult<T>
        {
            var result = await Evaluate(command as ICommand<T>);
            throw new NotImplementedException();
            //return new DomainResult<TResult>()
            //    .WithValue(result.IsSuccess
            //        ? result.Value as TResult
            //        : null)
            //    .WithReasons(result.Reasons);
        }

        public async Task<IEnumerable<IResult<ICommandResult<T>>>> Evaluate(params ICommand<T>[] commands)
        {
            var results = new List<IResult<ICommandResult<T>>>();
            foreach (var command in commands)
                results.Add(await Evaluate(command));
            return results;
        }
    }
}
