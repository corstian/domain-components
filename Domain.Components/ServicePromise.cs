using Domain.Components.Abstractions;

namespace Domain.Components
{
    public static class ServicePromise
    {
        // This method is solely developed to hide the internal "materialize" method as implemented on the `IPromise`.
        // The intent is to prevent random callers from unknowingly accessing an unevaluated service result.
        public static T Materialize<T>(IPromise<T> promise)
            where T : IServiceResult
            => promise.Materialize();
    }
}
