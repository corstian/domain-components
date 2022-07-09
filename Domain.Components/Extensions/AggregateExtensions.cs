using Domain.Components.Abstractions;

namespace Domain.Components.Extensions
{
    public static class AggregateExtensions
    {
        public static async Task<IResult<IEnumerable<IEvent<T>>>> EvaluateAndApply<T>(this IAggregate<T> aggregate, ICommand<T> command)
            where T : IAggregate<T>
        {
            var result = await aggregate.Evaluate(command);

            if (result.IsSuccess)
                await aggregate.Apply(result.Value.ToArray());

            return result;
        }

        public static async Task<IResult<R>> EvaluateAndApply<T, R>(this IAggregate<T> aggregate, ICommand<T, R> command)
            where T : IAggregate<T>
            where R : IMarkCommandOutput<T>
        {
            var result = await aggregate.Evaluate(command);

            if (result.IsSuccess)
            {
                switch (result.Value)
                {
                    case ICommandResult<T> commandResult:
                        await aggregate.Apply(commandResult);
                        break;
                    case IEvent<T> @event:
                        await aggregate.Apply(@event);
                        break;
                    default: break;
                }

            }

            return result;
        }

        public static async Task<IResult<R>> EvaluateTypedCommand<T, R>(this IAggregate<T> aggregate, ICommand<T, R> command)
            where T : IAggregate<T>
            where R : IMarkCommandOutput<T>
        {
            var result = await aggregate.Evaluate(command);

            return new DomainResult<R>()
                .WithValue(result.IsFailed
                    ? default
                    : result.Value)
                .WithReasons(result.Reasons);
        }

        public static async Task<IResult<R>> EvaluateTypedCommand<T, R>(this T aggregate, ICommand<T, R> command)
            where T : IAggregate<T>
            where R : IMarkCommandOutput<T>
        {
            var result = await aggregate.Evaluate(command);

            return new DomainResult<R>()
                .WithValue(result.IsFailed
                    ? default
                    : result.Value)
                .WithReasons(result.Reasons);
        }
    }
}
