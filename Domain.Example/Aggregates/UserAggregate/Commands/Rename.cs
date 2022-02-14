using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using FluentResults;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class Rename : ICommand<User, Renamed>
    {
        public string Name { get; init; }

        async Task<Result<Renamed>> ICommand<User, Renamed>.Evaluate(User handler)
            => Result.Ok(
                new Renamed
                {
                    Name = handler.Name
                });
    }
}
