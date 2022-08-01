using Domain.Components;
using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Example.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T>
        where T : Aggregate, IAggregate, new()
    {
        public MockRepository() { }

        private Dictionary<Guid, T> _dictionary = new();

        public Task<T> ById(Guid id) {
            if (!_dictionary.ContainsKey(id))
                _dictionary.Add(id, new T
                {
                    Id = id
                });

            return Task.FromResult(_dictionary.GetValueOrDefault(id)!);
        }
    }
}
