using FluentResults;

namespace Domain.Components
{
    internal static class DomainResultHelper
    {
        public static DomainResult Merge(IEnumerable<DomainResult> results)
        {
            return DomainResult.Ok().WithReasons(results.SelectMany(result => result.Reasons));
        }

        public static DomainResult<IEnumerable<TValue>> MergeWithValue<TValue>(IEnumerable<DomainResult<TValue>> results)
        {
            var finalResult = DomainResult.Ok<IEnumerable<TValue>>(new List<TValue>())
                .WithReasons(results.SelectMany(result => result.Reasons));

            if (finalResult.IsSuccess)
                finalResult.WithValue(results.Select(r => r.Value).ToList());

            return finalResult;
        }

        public static bool HasError<TError>(List<IError> errors, Func<TError, bool> predicate) where TError : IError
        {
            var anyErrors = errors.Any(error => error is TError errorOfTError && predicate(errorOfTError));
            if (anyErrors)
                return true;

            foreach (var error in errors)
            {
                var anyError = HasError(error.Reasons, predicate);
                if (anyError)
                    return true;
            }

            return false;
        }

        public static bool HasSuccess<TSuccess>(List<ISuccess> successes, Func<TSuccess, bool> predicate) where TSuccess : ISuccess
        {
            return successes.Any(success => success is TSuccess successOfTSuccess && predicate(successOfTSuccess));
        }
    }
}
