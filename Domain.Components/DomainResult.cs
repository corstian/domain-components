using Domain.Components.Abstractions;
using FluentResults;

namespace Domain.Components
{
    public class DomainResult : Result
    {
        /// <summary>
        /// Creates a success result
        /// </summary>
        public new static DomainResult Ok()
        {
            return new DomainResult();
        }

        /// <summary>
        /// Creates a failed result with the given error
        /// </summary>
        public new static DomainResult Fail(IError error)
        {
            var result = new DomainResult();
            result.WithError(error);
            return result;
        }

        /// <summary>
        /// Creates a failed result with the given error message. Internally an error object from type `Error` is created. 
        /// </summary>
        public new static DomainResult Fail(string errorMessage)
        {
            var result = new DomainResult();
            result.WithError(new Error(errorMessage));
            return result;
        }

        /// <summary>
        /// Creates a success result with the given value
        /// </summary>
        public new static DomainResult<TValue> Ok<TValue>(TValue value)
        {
            var result = new DomainResult<TValue>();
            result.WithValue(value);
            return result;
        }

        /// <summary>
        /// Creates a failed result with the given error
        /// </summary>
        public new static DomainResult<TValue> Fail<TValue>(IError error)
        {
            var result = new DomainResult<TValue>();
            result.WithError(error);
            return result;
        }

        /// <summary>
        /// Creates a failed result with the given error message. Internally an error object from type Error is created. 
        /// </summary>
        public new static DomainResult<TValue> Fail<TValue>(string errorMessage)
        {
            var result = new DomainResult<TValue>();
            result.WithError(new Error(errorMessage));
            return result;
        }

        /// <summary>
        /// Create a success/failed result depending on the parameter isSuccess
        /// </summary>
        public new static DomainResult OkIf(bool isSuccess, IError error)
        {
            return isSuccess ? Ok() : Fail(error);
        }

        /// <summary>
        /// Create a success/failed result depending on the parameter isSuccess
        /// </summary>
        public new static DomainResult OkIf(bool isSuccess, string error)
        {
            return isSuccess ? Ok() : Fail(error);
        }

        /// <summary>
        /// Create a success/failed result depending on the parameter isFailure
        /// </summary>
        public new static DomainResult FailIf(bool isFailure, IError error)
        {
            return isFailure ? Fail(error) : Ok();
        }

        /// <summary>
        /// Create a success/failed result depending on the parameter isFailure
        /// </summary>
        public new static DomainResult FailIf(bool isFailure, string error)
        {
            return isFailure ? Fail(error) : Ok();
        }
    }

    public class DomainResult<T> : Result<T>, IResult<T>
    {
        //public static DomainResult<T> Ok(T value)
        //{
        //    var result = new DomainResult<T>();
        //    result.WithValue(value);
        //    return result;
        //}

        //public static DomainResult<T> Fail(IError error)
        //{
        //    var result = new DomainResult<T>();
        //    result.WithError(error);
        //    return result;
        //}

        //public static DomainResult<T> Fail(string errorMessage)
        //{
        //    var result = new DomainResult<T>();
        //    result.WithError(new Error(errorMessage));
        //    return result;
        //}
    }
}
