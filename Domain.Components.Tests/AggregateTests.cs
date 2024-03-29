﻿using Domain.Components.Extensions;
using Domain.Components.Tests.Mocks;
using System;
using Xunit;

namespace Domain.Components.Tests
{
    public class AggregateTests
    {
        [Fact]
        public async void AggregateEventInteractopm()
        {
            var aggregate = new TestAggregate
            {
                Id = Guid.NewGuid()
            };

            var command = new TestCommand();

            var result = await aggregate.Evaluate(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(aggregate.Id, result.Value.AggregateId);
            Assert.Null(result.Value.AuthorizationContext);
        }

        [Fact]
        public async void InterfaceBasedAggregateEventInteraction()
        {
            var aggregate = new InterfaceAggregate
            {
                Id = Guid.NewGuid()
            };

            var command = new InterfaceCommand();

            var result = await aggregate.EvaluateTypedCommand(command);

            Assert.True(result.IsSuccess);
            Assert.Equal(Guid.Empty, result.Value.AggregateId);
        }
    }
}
