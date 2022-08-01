using Domain.Components.Abstractions;

namespace Domain.Components.Extensions
{
    public static class AggregateExtensions
    {
        public static Operation<TAggregate, TResult> LazilyEvaluate<TAggregate, TResult>(this TAggregate aggregate, ICommand<TAggregate, TResult> operation)
            where TAggregate : class, IAggregate<TAggregate>
            where TResult : ICommandResult<TAggregate>, new()
        {
            return new Operation<TAggregate, TResult>(aggregate, operation);
        }

        public static async Task<IResult<R>> EvaluateAndApply<T, R>(this IAggregate<T> aggregate, ICommand<T, R> command)
            where T : class, IAggregate<T>
            where R : ICommandResult<T>
        {
            var result = await aggregate.Evaluate(command);

            if (result.IsSuccess)
            {
                ICommandResult<T> commandResult = result.Value;

                await aggregate.Apply(commandResult);
            }

            return result;
        }

        public static async Task<IResult<R>> EvaluateTypedCommand<T, R>(this IAggregate<T> aggregate, ICommand<T, R> command)
            where T : class, IAggregate<T>
            where R : ICommandResult<T>
        {
            var result = await aggregate.Evaluate(command);

            return new DomainResult<R>()
                .WithValue(result.IsFailed
                    ? default
                    : result.Value)
                .WithReasons(result.Reasons);
        }

        public static async Task<IResult<R>> EvaluateTypedCommand<T, R>(this T aggregate, ICommand<T, R> command)
            where T : class, IAggregate<T>
            where R : ICommandResult<T>
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
