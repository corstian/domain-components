# Components for a solid domain
***Providing an opinionated interaction pattern against the domain***

> ℹ️ This repository contains practical and working examples of the things shown during the DDD Europe session ["How complex software impacts your cognitive abilities"](https://www.corstianboerman.com/blog/2022-06-24/how-complex-software-impacts-your-cognitive-abilities). The contents of this repository are very much experimental.

The `Domain.Components` consists of a number of classes and interfaces defining behavioural patterns for domain components. The goal of this library is to simplify domain development by constraining the domain's interaction patterns in a number of ways.

These constraints pay for themselves as the following properties and behaviours can be provided virtually free-of-charge:

- Event sourcing
- Scalability
- Authorization controls & logging
- Event consolidation (for disconnected clients)

## Why?
This library abstractly implements behaviour implemented over and over again in a wide variety of projects requiring architectural patterns relating to [Domain Driven Development (DDD)](https://en.wikipedia.org/wiki/Domain-driven_design), [Event Sourcing (ES)](https://en.wikipedia.org/wiki/Domain-driven_design#Event_sourcing) and [Command/Query Responsibility Segregation (CQRS)](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation).

The limited understanding and knowledge about these patterns by junior developers may prove to be troublesome for multiple reasons:

- Simpler CRUD style APIs and applications are more easily and cheaply built through no-code tools
- The implementation of these patterns through an incomplete and incoherent understanding leads to problems down the road, including unnecessary technical debt and complexity. Future development costs will greatly increase.

Having most of the default behaviour implemented through abstractions allows developers to focus on the business value provided through their applications, rather than technical details enabling the creation of this added value.

The architectural solidity this library aims to provide should be an enabling factor to be able to work according to the processes laid out through the [extreme programming (XP)](https://en.wikipedia.org/wiki/Test-driven_development) methodology.

## How?
The aim of this library is to build a domain completely separated from any other concerns of your application. Therefore this library does not provide anything related to persistence, event streams, or other infrastructure concerns. This way we are able to build a domain that is easily testable.

The library itself provides enough extension points to wire it up with your favourite infrastructure projects. The most straightforward way to use this library in production is to depend on Orleans.net to provide these infrastructure concerns. The additional bonus you'll have is that the domain itself will be scalable to planet-size proportions by default.

## Getting started
The `Domain.Components` package contains base classes and interface to define a number of components:

- Commands & Events
- Aggregates
- Services
- Policies & Sagas
- Process Managers

These components will cover almost all of the behavioural requirements expected from a properly designed domain. This project contains both interfaces and abstract implementations for all of these types. If you're starting from scratch it is recommended to derive your implementations from these abstract implementations. Otherwise you might want to use the interfaces such that you can adapt pre-existing logic to fit within the interaction pattern provided by this project.

### The aggregate
The aggregate is perhaps the most important unit within a domain. It's the sole component you'll be able to directly mutate the state of. The aggregate itself will only hold the state itself. 

```csharp
// The abstract Aggregate class is self-referential to be able to provide certain behaviour out of the box
public class User : Aggregate<User>
{
    public string Name { get; internal set; }
    public string Email { get; internal set; }
}
```

### Events
The way this state can be modified is through the application of an event. The event holds the data required to complete the modification, and handles the modification of the aggregate:

```csharp
public class NameChanged : Event<User>
{
    internal NameChanged() { }
    
    public string Name { get; init; }

    public override void Apply(User state)
    {
        state.Name = Name;
    }
}

public class EmailChanged : Event<User>
{
    internal EmailChanged() { }

    public string Email { get; init; }

    public override void Apply(User state)
    {
        state.Email = Email;
    }
}
```

There are three important principles to be found in the snippet above:
1. The events derive from an abstract `Event` referencing the aggregate to which it can be applied.
2. The constructors are marked internal, making the domain itself responsible for instantiating new instances of these events. This responsibility will be carried by the commands, we'll cover next.
3. The operation to change the `User` aggregate is represented through two distinctive events. The reason to this is to future-proof the application; to make future changes easier. [You can find more about the underlying mental concept here.](https://www.corstianboerman.com/blog/2022-01-27/coarse-commands-emitting-granular-events)

### Commands
The next thing required to modify the aggregate is the command. The command is responsible for two things:

1. The validation of input parameters against the current state of the aggregate.
2. The instantiation of events in case this previous step is succesful.

A command might look like this:

```csharp
public class UpdateUserInfo : Command<User, NameChanged, EmailChanged>
{
    public string Name { get; init; }
    public string Email { get; init; }

    public override DomainResult<EmailChanged> Evaluate(User handler)
    {
        if (!Email.Contains("@"))
            return DomainResult.Fail<(NameChanged, EmailChanged)>("No @");
        
        return DomainResult.Ok((
            new NameChanged
            {
                Name = Name
            },
            new EmailChanged
            {
                Email = Email
            }));
    }
}
```

These are the important bits to know about commands:
1. The abstract `Command` class accepts generic arguments describing the aggregate it may be used on, as well as the event type(s) that are being returned upon succesful evaluation. The number of events returned may be zero, one or more.
2.  The command may return an `DomainResult` object describing one or more errors which occured during evaluation.

### Usage
Now that we have all the dependencies in place we may use the command in the following manner:

```csharp
var user = new User();

var command = new UpdateUserInfo {
    Name = "John Doe",
    Email = "john.doe@example.com"
};

var result = user.Evaluate(command);

if (result.IsSuccess) {
    user.Apply(result.Value.Item1); // NameChanged
    user.Apply(result.Value.Item2); // EmailChanged
}
```

### Test driven development
Not only can the domain be implemented in a way that is this easy, but unit tests may be constructed in exactly the same manner. During development it allows you to construct a collection of tests playing around with the various domain objects. This brings us to the essence of [test driven development (TDD)](https://en.wikipedia.org/wiki/Test-driven_development), wherein it should be possible to write the test cases before implementing the actual behaviour of the program. As the test suite grows the behaviour of the domain should be ever more clearly defined.
