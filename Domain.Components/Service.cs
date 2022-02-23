using Domain.Components.Abstractions;
using FluentResults;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Components
{
    public abstract class Service<TService> : IService<TService>
        where TService : Service<TService>
    {
        internal readonly IServiceProvider Services;
        public Service(IServiceProvider services)
        {
            Services = services;
        }

        public async Task Commit(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                var eventInterface = @event.GetType()
                    .GetInterfaces()
                    .SingleOrDefault(q => q.IsGenericType
                        && q.GetGenericTypeDefinition() == typeof(IEvent<>));

                if (eventInterface == null)
                    throw new Exception("Provided type does not implement IEvent<>");

                var repository = (IRepository<IAggregate>)Services
                    .GetRequiredService(
                        typeof(IRepository<>)
                        .MakeGenericType(eventInterface.GetGenericArguments()[0]));

                var aggregate = repository
                    .ById(@event.AggregateId);

                /*
                 * Since the repository has the same generic type as the event, we can be sure the 
                 * aggregate instance also has the same generic. Using reflection we should be able
                 * to apply the event to the aggregate.
                 */
                aggregate
                    .GetType()
                    .GetMethod("Apply")
                    .Invoke(aggregate, new[] { @event });
            }
        }

        public async Task<S> Commit<T, S>(IEvent<T> @event)
            where T : IAggregate<T>
            where S : ISnapshot<T>, new()
        {
            var repository = Services.GetRequiredService<IRepository<T>>();
            var aggregate = repository.ById(@event.AggregateId);

            return await aggregate.Apply<S>(@event);
        }

        public async Task<(S1, S2)> Commit<T1, T2, S1, S2>((IEvent<T1>, IEvent<T2>) events)
            where T1 : IAggregate<T1>
            where T2 : IAggregate<T2>
            where S1 : ISnapshot<T1>, new()
            where S2 : ISnapshot<T2>, new()
            => (await Commit<T1, S1>(events.Item1),
                await Commit<T2, S2>(events.Item2));

        public Task Commit(IEvent @event)
        {
            throw new NotImplementedException();
        }

        public Task Commit((IEvent, IEvent) events)
        {
            throw new NotImplementedException();
        }

        Task IService<TService>.Commit(params IEvent[] events)
        {
            throw new NotImplementedException();
        }

        Task<S> IService<TService>.Commit<T, S>(IEvent<T> @event)
        {
            throw new NotImplementedException();
        }

        async Task<(S1, S2)> IService<TService>.Commit<T1, T2, S1, S2>((IEvent<T1>, IEvent<T2>) events)
            => (await Commit<T1, S1>(events.Item1),
                await Commit<T2, S2>(events.Item2));

        Task<IResult<IEnumerable<IEvent>>> IService<TService>.Evaluate(IServiceCommand<TService> command)
            => command.Evaluate((TService)this);
    }
}
