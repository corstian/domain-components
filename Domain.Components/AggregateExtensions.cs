using Domain.Components.Abstractions;

namespace Domain.Components
{
    public static class AggregateExtensions
    {
        public static IResult<E> EvaluateTypedCommand<T, E>(this T aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = command.Evaluate(aggregate);

            if (result.ValueOrDefault is Event e)
                e.AggregateId = aggregate.Id;

            return new DomainResult<E>()
                .WithReasons(result.Reasons)
                .WithValue(result.ValueOrDefault);
        }

        public static IResult<(E1, E2)> EvaluateTypedCommand<T, E1, E2>(this T aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = command.Evaluate(aggregate);

            if (result.ValueOrDefault.Item1 is Event e1)
                e1.AggregateId = aggregate.Id;
            if (result.ValueOrDefault.Item2 is Event e2)
                e2.AggregateId = aggregate.Id;

            return new DomainResult<(E1, E2)>()
                .WithReasons(result.Reasons)
                .WithValue(result.ValueOrDefault);
        }
    }
}
