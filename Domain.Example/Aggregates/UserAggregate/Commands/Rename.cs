using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class Rename : ICommand<User, Renamed>
    {
        public string Name { get; init; }

        Task<Renamed> ICommand<User, Renamed>.Evaluate(User handler)
            => Task.FromResult(new Renamed
            {
                Name = handler.Name
            });
    }
}
