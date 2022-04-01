using Domain.Components.Abstractions;
using System.Reactive.Subjects;

namespace Domain.Example.Tests.Mocks
{
    internal class MockEventBus
    {
        public Subject<IEvent> EventStream { get; } = new Subject<IEvent> ();

        public void Publish(IEvent @event)
        {
            EventStream.OnNext(@event);
        }
    }
}
