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
                .EvaluateOperation();

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.ValueOrDefault);
            Assert.IsType<TestAggregate>(result.Value.Single().Aggregate);
            Assert.IsType<TestEvent>(result.Value.Single().Events.Single());

            var package = result.Value.Single();

            await package.Apply();

            Assert.Equal(1, aggregate.EventsApplied);
        }
    }
}
