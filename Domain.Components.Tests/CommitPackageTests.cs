using Domain.Components.Abstractions;
using Domain.Components.Extensions;
using Domain.Components.Tests.Mocks;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Components.Tests
{
    public class CommitPackageTests
    {
        [Fact]
        public void TestFactory()
        {
            var factory = new CommitPackagesFactory();

            var testAggregate = new TestAggregate();
            var interfaceAggregate = new InterfaceAggregate();

            factory
                .AddCommitPackage(
                    testAggregate,
                    builder => builder
                        .IncludeCommand(new TestCommand()))
                .AddCommitPackage(
                    interfaceAggregate,
                    builder => builder
                        .IncludeCommand(new InterfaceCommand()));
        }

        [Fact]
        public async Task SimplesCommitPackagesFactoryTest()
        {
            var aggregate = new TestAggregate();

            var result = await new CommitPackagesFactory()
                .AddCommitPackage(
                    aggregate, 
                    builder => builder
                        .IncludeCommand(new TestCommand()))
                .Evaluate();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ValueOrDefault);
            Assert.IsType<TestAggregate>(result.Value.Single().Aggregate);
            Assert.IsType<TestEvent>(result.Value.Single().Events.Single());

            var package = result.Value.Single();

            await package.Apply();

            Assert.Equal(1, aggregate.EventsApplied);
        }

        /*
         * It seems like the proxy approach is rather slow due to its dependence on reflection and a number of linq queries.
         * When bored figure out if there's an approach through which we can optimize the performance of this behaviour.
         */
        [Fact]
        public async Task ProxyShouldWorkWithCommitPackage()
        {
            var aggregate = new TestAggregate();
            var proxy = new AggregateProxy<TestAggregate>(aggregate);

            var result = await new CommitPackagesFactory()
                .AddCommitPackage(
                    proxy,
                    builder => builder
                        .IncludeCommand(new TestCommand()))
                .Evaluate();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ValueOrDefault);
            Assert.True(result.Value.Single().Aggregate is IAggregate<TestAggregate>);
            Assert.IsType<TestEvent>(result.Value.Single().Events.Single());

            /*
             * Please note that when using an interface rather than the actual aggregate,
             * one is unable to access the internal state of the aggregate without the use of
             * a snapshot. Though one most likely does not need to access this internal state,
             * it's something to take into consideration when designing unit tests.
             */

            var package = result.Value.Single();

            await package.Apply();

            Assert.Equal(1, aggregate.EventsApplied);
        }

        [Fact]
        public async Task CommitPackageTest()
        {
            var proxy = new AggregateProxy<TestAggregate>();

            var res = new CommitPackage<TestAggregate>(
                proxy,
                new[] { new TestEvent() });
        }
    }
}
