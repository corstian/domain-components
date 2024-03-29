﻿using Domain.Components.Abstractions;

namespace Domain.Components.Tests.Mocks
{
    public class TestCommand : Command, ICommand<TestAggregate, TestEvent>
    {
        IResult<TestEvent> ICommand<TestAggregate, TestEvent>.Evaluate(TestAggregate handler)
            => DomainResult.Ok(new TestEvent());
    }
}
