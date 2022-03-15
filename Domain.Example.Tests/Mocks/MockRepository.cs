using Domain.Components;
using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;

namespace Domain.Example.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T>
        where T : Aggregate, new()
    {
        public MockRepository() { }

        private Dictionary<Guid, T> _dictionary = new();

        public T ById(Guid id) {
            if (!_dictionary.ContainsKey(id))
                _dictionary.Add(id, new T
                {
                    Id = id
                });

            return _dictionary.GetValueOrDefault(id)!;
        }
    }
}
