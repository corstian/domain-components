using Domain.Components.Abstractions;
using System;

namespace Domain.Example.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T>
        where T : IAggregate, new()
    {
        public T ById(Guid id) => new();
    }
}
