﻿using Domain.Components.Abstractions;
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

        public static async Task<IResult<IEnumerable<IEvent>>> Evaluate(this IAggregate aggregate, ICommand command)
        {
            /*
             * The non-standard technique of getting a method used here is to handle an explicit implementation
             * of the `IAggregate<T>` interface. (For example on proxy objects)
             */

            var methods = aggregate
                .GetType()
                .GetMethods(BindingFlags.Public
                    | BindingFlags.NonPublic
                    | BindingFlags.Instance);

            var method = methods.First(q => q.Name.EndsWith("Evaluate") && !q.ContainsGenericParameters);

            return (IResult<IEnumerable<IEvent>>)await method.InvokeAsync(aggregate, new[] { command });
        }

        public static async Task Apply(this IAggregate aggregate, IEvent @event)
        {
            var method = aggregate
                .GetType()
                .GetMethods()
                .Single(q => q.Name == "Apply"
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
            {
                await aggregate.Apply(@event);
            }
        }

        //public static async Task<TModel> Apply<TModel>(this IAggregate aggregate, IEvent @event)
        //    where TModel : ISnapshot, new()
        //{
        //    var method = aggregate
        //        .GetType()
        //        .GetMethods()
        //        .Single(q => q.Name == "Apply"
        //            && q.IsGenericMethod
        //            && q.GetParameters()
        //                .Any(w => w.ParameterType
        //                    .GetInterfaces()
        //                    .Contains(typeof(IEvent))));

        //    return (TModel)await method.InvokeAsync(aggregate, new[] { @event });
        //}

        //public static async Task<TModel> Apply<TModel>(this IAggregate aggregate, params IEvent[] events)
        //    where TModel : ISnapshot, new()
        //{
        //    foreach (var @event in events)
        //        aggregate.Apply(@event);
        //}
    }
}
