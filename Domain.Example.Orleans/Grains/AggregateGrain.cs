using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans.EventSourcing;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrain<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        private readonly ILogger _logger;

        public AggregateGrain(ILogger<AggregateGrain<T>> logger)
        {
            _logger = logger;
        }

        public async Task Apply(IEvent<T> @event)
        {
            await State.Apply(@event);
            await ConfirmEvents();
            _logger.LogInformation("Event applied: {event}", @event);
        }

        public async Task Apply(params IEvent<T>[] events)
        {
            await State.Apply(events);
            await ConfirmEvents();
            _logger.LogInformation("Events applied: {events}", events);
        }

        public async Task<TModel> Apply<TModel>(IEvent<T> @event) where TModel : ISnapshot<T>, new()
        {
            var snapshot = await State.Apply<TModel>(@event);
            await ConfirmEvents();
            return snapshot;
        }

        public async Task<TModel> Apply<TModel>(params IEvent<T>[] events) where TModel : ISnapshot<T>, new()
        {
            var snapshot = await State.Apply<TModel>(events);
            await ConfirmEvents();
            return snapshot;
        }

        public async Task<IResult<IEnumerable<IEvent<T>>>> Evaluate(ICommand<T> command)
        {
            var result = await State.Evaluate(command);

            if (result.IsSuccess)
                _logger.LogInformation("Command evaluation succesful: {command}", command);
            else
                _logger.LogWarning("Command evaluation failed\r\nCommand: {command}\r\nReasons: {reasons}", command, result.Reasons);

            return result;
        }
    }
}
