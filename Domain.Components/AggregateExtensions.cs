using Domain.Components.Abstractions;

namespace Domain.Components
{
    public static class AggregateExtensions
    {
        public static IResult<E> EvaluateTypedCommand<T, E>(this IAggregate<T> aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            var @event = result.ValueOrDefault.ElementAt(0);

            if (@event is Event e)
                e.AggregateId = aggregate.Id;

            return new DomainResult<E>()
                .WithReasons(result.Reasons)
                .WithValue((E)@event);
        }

        public static IResult<E> EvaluateTypedCommand<T, E>(this T aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            var @event = result.ValueOrDefault.ElementAt(0);

            if (@event is Event e)
                e.AggregateId = aggregate.Id;

            return new DomainResult<E>()
                .WithReasons(result.Reasons)
                .WithValue((E)@event);
        }

        public static IResult<(E1, E2)> EvaluateTypedCommand<T, E1, E2>(this IAggregate<T> aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            if (result.ValueOrDefault.ElementAt(0) is Event e1)
                e1.AggregateId = aggregate.Id;
            if (result.ValueOrDefault.ElementAt(1) is Event e2)
                e2.AggregateId = aggregate.Id;

            return new DomainResult<(E1, E2)>()
                .WithReasons(result.Reasons)
                .WithValue((
                    (E1)result.ValueOrDefault.ElementAt(0),
                    (E2)result.ValueOrDefault.ElementAt(1)
                ));
        }

        public static IResult<(E1, E2)> EvaluateTypedCommand<T, E1, E2>(this T aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            if (result.ValueOrDefault.ElementAt(0) is Event e1)
                e1.AggregateId = aggregate.Id;
            if (result.ValueOrDefault.ElementAt(1) is Event e2)
                e2.AggregateId = aggregate.Id;

            return new DomainResult<(E1, E2)>()
                .WithReasons(result.Reasons)
                .WithValue((
                    (E1)result.ValueOrDefault.ElementAt(0),
                    (E2)result.ValueOrDefault.ElementAt(1)
                ));
        }
    }
}
