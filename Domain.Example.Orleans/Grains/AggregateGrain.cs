using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Streams;

namespace Domain.Example.Orleans.Grains
{
    public class AggregateGrain<T> : JournaledGrain<T, IEvent<T>>, IAggregateGrain<T>
        where T : class, IAggregate<T>, new()
    {
        private readonly ILogger _logger;
        private IAsyncStream<IEvent<T>> _stream = null;

        public Guid Id { get; init; }

        public AggregateGrain(ILogger<AggregateGrain<T>> logger)
        {
            _logger = logger;
        }

        public override Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("stream");

            _stream = streamProvider.GetStream<IEvent<T>>(this.GetPrimaryKey(), typeof(T).Name);

            return base.OnActivateAsync();
        }

        public async Task Apply(IEvent<T> @event)
        {
            await State.Apply(@event);
            await ConfirmEvents();
            await _stream.OnNextAsync(@event);
            _logger.LogInformation("Event applied: {event}", @event);
        }

        public async Task Apply(params IEvent<T>[] events)
        {
            await State.Apply(events);

            await ConfirmEvents();
            await _stream.OnNextBatchAsync(events);
            _logger.LogInformation("Events applied: {events}", events);
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

        public async Task<TModel> GetSnapshot<TModel>() where TModel : ISnapshot<T>, new()
        {
            await ConfirmEvents();
            return await State.GetSnapshot<TModel>();
        }
    }
}

