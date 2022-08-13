using Domain.Components.Abstractions;
using System;
using System.Collections.Generic;

namespace Domain.Example.Tests.Mocks
{
    public class MockAggregateProvider : IAggregateProvider
    {
        private Dictionary<Guid, object> _dictionary = new();

        IAggregate<TAggregate> IAggregateProvider.Get<TAggregate>(Guid id)
        {
            TAggregate aggregate = null;

            if (!_dictionary.ContainsKey(id))
            {
                aggregate = Activator.CreateInstance(typeof(TAggregate)) as TAggregate;

                _dictionary.Add(id, aggregate);
            }

            return aggregate;
        }
    }
}
