using Domain.Components.Abstractions;
using System.Reflection;

namespace Domain.Components.Extensions
{
    public static class ExtensionMethods
    {
        public static async Task<object> InvokeAsync(this MethodInfo @this, object obj, params object[] parameters)
        {
            var task = (Task)@this.Invoke(obj, parameters);
            await task.ConfigureAwait(false);
            var resultProperty = task.GetType().GetProperty("Result");
            return resultProperty.GetValue(task);
        }

        internal static IEnumerable<IOperation> OperationsFromServiceResults(this IEnumerable<IServiceResult> results)
        {
            foreach (var result in results)
                switch (result)
                {
                    case IOperation operation:
                        yield return operation;
                        break;

                    case IServiceResult:
                        foreach (var operation in OperationsFromServiceResults(result.Operations))
                            yield return operation;
                        break;
                }
        }

        // ToDo: Ensure the group operation is not asynchronous
        internal static IEnumerable<IGrouping<string, IOperation>>
            Group(this IEnumerable<IOperation> operations)
            => operations
                .ToList()
                .GroupBy(q => q.Aggregate.GetIdentity().Result);
    }
}
