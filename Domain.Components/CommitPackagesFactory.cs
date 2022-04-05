using Domain.Components.Abstractions;
using Domain.Components.Extensions;
using FluentResults;

namespace Domain.Components
{
    public class CommitPackagesFactory
    {
        private int _index = 0;
        private Dictionary<int, ICommitPackageBuilder> _builders = new();
        private Dictionary<int, Task<IResult<IEnumerable<ICommitPackage>>>> _serviceCommands = new();

        public CommitPackagesFactory AddCommitPackage<TAggregate>(
            IAggregate<TAggregate> aggregate,
            Action<CommitPackageBuilder<TAggregate>> builder)
            where TAggregate : IAggregate<TAggregate>
        {
            var packageBuilder = new CommitPackageBuilder<TAggregate>
            {
                Aggregate = aggregate
            };

            builder.Invoke(packageBuilder);

            _builders.Add(_index++, packageBuilder);

            return this;
        }

        public CommitPackagesFactory AddService(
            Task<IResult<IEnumerable<ICommitPackage>>> promise)
        {
            _serviceCommands.Add(_index++, promise);
            
            return this;
        }

        public async Task<IResult<IEnumerable<ICommitPackage>>> Evaluate()
        {
            List<ICommitPackage> packages = new();
            List<IReason> reasons = new();

            for (var i = 0; i < _index; i++)
            {
                if (_builders.TryGetValue(i, out var builder))
                {
                    (ICommitPackage package, IReason[] reason) = await ProcessBuilder(builder);

                    packages.Add(package);
                    reasons.AddRange(reason);
                } 
                else if (_serviceCommands.TryGetValue(i, out var serviceCommand))
                {
                    var result = await serviceCommand;

                    reasons.AddRange(result.Reasons);
                    packages.AddRange(result.ValueOrDefault);
                }
            }

            if (reasons.OfType<IError>().Any())
                return new DomainResult<IEnumerable<ICommitPackage>>()
                    .WithReasons(reasons);

            return new DomainResult<IEnumerable<ICommitPackage>>()
                .WithValue(packages)
                .WithReasons(reasons);
        }

        private async Task<(ICommitPackage? package, IReason[] reasons)> ProcessBuilder(ICommitPackageBuilder builder)
        {
            List<IResult<IEnumerable<IEvent>>> results = new();

            foreach (var command in builder.Commands)
                results.Add(await builder.Aggregate.Evaluate(command));

            if (results.Any(q => q.IsFailed))
                return (null, results.SelectMany(q => q.Reasons).ToArray());

            var generic = builder.GetType().GetGenericArguments()[0];

            var genericType = typeof(CommitPackage<>).MakeGenericType(generic);
            var package = Activator.CreateInstance(genericType) as ICommitPackage;

            genericType
                .GetProperty("Aggregate")
                .SetValue(package, builder.Aggregate);

            var eventList = genericType
                .GetProperty("Events")
                .GetValue(package);

            var addEventToList = eventList
                .GetType()
                .GetMethod("Add");

            /*
             * Direct assignment of the events to an enumerable does not work. As a workaroudn
             * we're looping through the events and applying each individual item to the list instead.
             */
            foreach (var @event in results.SelectMany(q => q.Value))
                addEventToList.Invoke(eventList, new[] { @event });

            return (package, new IReason[0]);
        }
    }
}
