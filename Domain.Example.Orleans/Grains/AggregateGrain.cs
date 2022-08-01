using Domain.Components.Abstractions;
using Domain.Example.Orleans.Interfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Providers;
using Orleans.Streams;

namespace Domain.Example.Orleans.Grains
{
    [LogConsistencyProvider(ProviderName = "LogStorage")]
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

        public override async Task OnActivateAsync()
        {
            var streamProvider = GetStreamProvider("stream");

            _stream = streamProvider.GetStream<IEvent<T>>(this.GetPrimaryKey(), typeof(T).Name);

            await RefreshNow();

            await base.OnActivateAsync();
        }

        public async Task Apply(IEvent<T> @event)
        {
            RaiseEvent(@event);

            await ConfirmEvents();
            await _stream.OnNextAsync(@event);
            _logger.LogInformation("Event applied: {event}", @event);
        }

        public async Task Apply(params IEvent<T>[] events)
        {
            RaiseEvents(events);

            await ConfirmEvents();
            // Not supported...
            //await _stream.OnNextBatchAsync(events);
            
            foreach (var @event in events)
                await _stream.OnNextAsync(@event);
            
            _logger.LogInformation("Events applied: {events}", events);
        }

        public async Task<TModel> GetSnapshot<TModel>() where TModel : ISnapshot<T>, new()
        {
            await ConfirmEvents();
            return await State.GetSnapshot<TModel>();
        }

        public Task<IResult<TResult>> Evaluate<TResult>(ICommand<T, TResult> command) 
            where TResult : ICommandResult<T>
        {
            throw new NotImplementedException();
        }

        public Task Apply(ICommandResult<T> commandResult)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetIdentity()
            => Task.FromResult(Id.ToString());
    }
}
