namespace Domain.Components.Abstractions
{
    public class CommitPackagesFactory
    {
        private List<CommitPackageBuilder> builders = new();

        public CommitPackagesFactory AddCommitPackage<TAggregate>(Action<CommitPackageBuilder<TAggregate>> builder)
            where TAggregate : IAggregate<TAggregate>
        {
            var packageBuilder = new CommitPackageBuilder<TAggregate>();

            builder.Invoke(packageBuilder);

            return this;
        }
    }
}
