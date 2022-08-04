using Domain.Components.Abstractions;
using System.Reflection;

namespace Domain.Components.Extensions
{
    public static class IAggregateExtensions
    {
        /*
         * Originally I had been working with default interface methods, though that didn't
         * work nicely with Orleans. See https://github.com/dotnet/orleans/issues/7630 for a bug report.
         * 
         * To work around that we're using good old reflection techniques
         */

        public static async Task<IResult<ICommandResult>> Evaluate(this IAggregate aggregate, ICommand command)
        {
            /*
             * The non-standard technique of getting a method used here is to handle an explicit implementation
             * of the `IAggregate<T>` interface. (For example on proxy objects)
             */

            var method = aggregate
                .GetType()
                .GetMethods(BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance)
                .First(q => q.Name.EndsWith("Evaluate") // ToDo: Fix this endswith with something more solid
                    && !q.ContainsGenericParameters
                    && !q.ReturnParameter.ToString().Contains("IEnumerable"));

            return (IResult<ICommandResult>)await method.InvokeAsync(aggregate, new[] { command });
        }

        public static async Task<IEnumerable<IResult<ICommandResult>>> Evaluate(this IAggregate aggregate, IEnumerable<ICommand> commands)
        {
            var method = aggregate
                .GetType()
                .GetMethods(BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance)
                .FirstOrDefault(q => q.Name.EndsWith("Evaluate") // ToDo: Fix this endswith with something more solid
                    && !q.ContainsGenericParameters
                    && q.ReturnParameter.ToString().Contains("IEnumerable"));

            // If this function is not implemented on the type we'll try to find the interface
            // and use the default interface implementation.
            if (method == null)
            {
                method = aggregate
                    .GetType()
                    .GetInterfaces()
                    .First(q => q.IsGenericType)
                    .GetMethods()
                    .First(q => q.Name == "Evaluate"
                        && q.ReturnParameter.ToString().Contains("IEnumerable"));
            }

            //return (IEnumerable<IResult<ICommandResult>>)await method.InvokeAsync(aggregate, new[] { commands.ToArray() });


            // Foreach command, check if it is an `ICommand<T>` with T being
            var type = method
                .GetParameters()[0]
                .ParameterType
                .UnderlyingSystemType
                .GetGenericArguments();

            var generic = typeof(ICommand<>).MakeGenericType(type);

            var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(generic));

            foreach (var command in commands)
            {
                list
                    .GetType()
                    .GetMethod("Add")
                    .Invoke(list, new[] { command });
            }

            var result = await method.InvokeAsync(aggregate, new[]
            {
                list
                    .GetType()
                    .GetMethod("ToArray")
                    .Invoke(list, null)
            });

            return (IEnumerable<IResult<ICommandResult>>)result;
        }



        public static async Task Apply(this IAggregate aggregate, IEvent @event)
        {
            var method = aggregate
                .GetType()
                .GetMethods(BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance)
                .Single(q => q.Name.EndsWith("Apply") // ToDo: Fix this endswith with something more solid
                    && !q.IsGenericMethod
                    && q.GetParameters()
                        .Any(w => w.ParameterType
                            .GetInterfaces()
                            .Contains(typeof(IEvent))));

            await method.InvokeAsync(aggregate, new[] { @event });
        }

        public static async Task Apply(this IAggregate aggregate, params IEvent[] events)
        {
            foreach (var @event in events)
                await aggregate.Apply(@event);
        }
    }
}
