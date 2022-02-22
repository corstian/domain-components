using Domain.Components.Abstractions;

namespace Domain.Components
{
    public static class AggregateExtensions
    {
        public static async Task<IResult<E>> EvaluateTypedCommand<T, E>(this IAggregate<T> aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            var @event = result.ValueOrDefault.ElementAt(0);

            return new DomainResult<E>()
                .WithReasons(result.Reasons)
                .WithValue((E)@event);
        }

        public static async Task<IResult<E>> EvaluateTypedCommand<T, E>(this T aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            var @event = result.ValueOrDefault.ElementAt(0);

            return new DomainResult<E>()
                .WithReasons(result.Reasons)
                .WithValue((E)@event);
        }

        public static async Task<IResult<(E1, E2)>> EvaluateTypedCommand<T, E1, E2>(this IAggregate<T> aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            return new DomainResult<(E1, E2)>()
                .WithReasons(result.Reasons)
                .WithValue((
                    (E1)result.ValueOrDefault.ElementAt(0),
                    (E2)result.ValueOrDefault.ElementAt(1)
                ));
        }

        public static async Task<IResult<(E1, E2)>> EvaluateTypedCommand<T, E1, E2>(this T aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            return new DomainResult<(E1, E2)>()
                .WithReasons(result.Reasons)
                .WithValue((
                    (E1)result.ValueOrDefault.ElementAt(0),
                    (E2)result.ValueOrDefault.ElementAt(1)
                ));
        }
    }
}
