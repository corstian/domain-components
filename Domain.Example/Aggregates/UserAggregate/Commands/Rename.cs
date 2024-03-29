﻿using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class Rename : ICommand<User, Renamed>
    {
        public string Name { get; init; } = "";

        IResult<Renamed> ICommand<User, Renamed>.Evaluate(User handler)
        {
            return DomainResult.Ok(new Renamed
            {
                Name = Name
            });
        }
    }
}
