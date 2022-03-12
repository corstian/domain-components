using Domain.Components.Abstractions;
using FluentResults;

namespace Domain.Components
{
    public class CommitPackagesFactory
    {
        private List<ICommitPackageBuilder> _builders = new();

        public CommitPackagesFactory AddCommitPackage<TAggregate>(
            TAggregate aggregate,
            Action<CommitPackageBuilder<TAggregate>> builder)
            where TAggregate : IAggregate<TAggregate>
        {
            var packageBuilder = new CommitPackageBuilder<TAggregate>
            {
                Aggregate = aggregate
            };

            builder.Invoke(packageBuilder);

            _builders.Add(packageBuilder);

            return this;
        }

        public async Task<IResult<IEnumerable<ICommitPackage>>> EvaluateOperation()
        {
            bool isFailed = false;
            List<IReason> reasons = new();
            List<ICommitPackage> packages = new();

            foreach (var builder in _builders)
            {
                List<IResult<IEnumerable<IEvent>>> results = new();

                foreach (var command in builder.Commands)
                {
                    var eres = await builder.Aggregate.Evaluate(command);
                    results.Add(eres);
                }

                if (results.Any(q => q.IsFailed))
                {
                    reasons.AddRange(results.SelectMany(q => q.Reasons));
                    continue;
                }

                var genericType = typeof(CommitPackage<>).MakeGenericType(builder.Aggregate.GetType());
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
                    addEventToList.Invoke(eventList, new [] { @event });

                packages.Add(package);
            }

            if (reasons.OfType<IError>().Any())
                return new DomainResult<IEnumerable<ICommitPackage>>()
                    .WithReasons(reasons);

            return new DomainResult<IEnumerable<ICommitPackage>>()
                .WithValue(packages)
                .WithReasons(reasons);
        }
    }
}
