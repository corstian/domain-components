﻿using Domain.Components.Abstractions;

namespace Domain.Components
{
    public static class AggregateExtensions
    {
        public static async Task<IResult<E>> EvaluateTypedCommand<T, E>(this IAggregate<T> aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            return new DomainResult<E>()
                .WithValue(result.IsFailed 
                    ? default 
                    : (E)result.Value.ElementAt(0))
                .WithReasons(result.Reasons);
        }

        public static async Task<IResult<E>> EvaluateTypedCommand<T, E>(this T aggregate, ICommand<T, E> command)
            where T : IAggregate<T>
            where E : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E>(command));

            return new DomainResult<E>()
                .WithValue(result.IsFailed 
                    ? default 
                    : (E)result.Value.ElementAt(0))
                .WithReasons(result.Reasons);
        }

        public static async Task<IResult<(E1, E2)>> EvaluateTypedCommand<T, E1, E2>(this IAggregate<T> aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            return new DomainResult<(E1, E2)>()
                .WithValue(result.IsFailed 
                    ? default 
                    : ((E1)result.Value.ElementAt(0),
                       (E2)result.Value.ElementAt(1)
                    ))
                .WithReasons(result.Reasons);
        }

        public static async Task<IResult<(E1, E2)>> EvaluateTypedCommand<T, E1, E2>(this T aggregate, ICommand<T, E1, E2> command)
            where T : IAggregate<T>
            where E1 : IEvent<T>
            where E2 : IEvent<T>
        {
            var result = await aggregate.Evaluate(new GenericCommandWrapper<T, E1, E2>(command));

            return new DomainResult<(E1, E2)>()
                .WithValue(result.IsFailed 
                    ? default 
                    : ((E1)result.Value.ElementAt(0),
                       (E2)result.Value.ElementAt(1)
                    ))
                .WithReasons(result.Reasons);
        }
    }
}
