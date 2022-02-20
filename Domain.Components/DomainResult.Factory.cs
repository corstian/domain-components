using FluentResults;

namespace Domain.Components
{
    public partial class DomainResult
    {
        internal static ResultSettings Settings { get; private set; }

        static DomainResult()
        {
            Settings = new ResultSettingsBuilder().Build();
        }

        /// <summary>
        /// Setup global settings like logging
        /// </summary>
        public static void Setup(Action<ResultSettingsBuilder> setupFunc)
        {
            var settingsBuilder = new ResultSettingsBuilder();
            setupFunc(settingsBuilder);

            Settings = settingsBuilder.Build();
        }

        /// <summary>
        /// Creates a success DomainResult
        /// </summary>
        public static DomainResult Ok()
        {
            return new DomainResult();
        }

        /// <summary>
        /// Creates a failed DomainResult with the given error
        /// </summary>
        public static DomainResult Fail(IError error)
        {
            var DomainResult = new DomainResult();
            DomainResult.WithError(error);
            return DomainResult;
        }

        /// <summary>
        /// Creates a failed DomainResult with the given error message. Internally an error object from type `Error` is created. 
        /// </summary>
        public static DomainResult Fail(string errorMessage)
        {
            var DomainResult = new DomainResult();
            DomainResult.WithError(new Error(errorMessage));
            return DomainResult;
        }

        /// <summary>
        /// Creates a success DomainResult with the given value
        /// </summary>
        public static DomainResult<TValue> Ok<TValue>(TValue value)
        {
            var DomainResult = new DomainResult<TValue>();
            DomainResult.WithValue(value);
            return DomainResult;
        }

        /// <summary>
        /// Creates a failed DomainResult with the given error
        /// </summary>
        public static DomainResult<TValue> Fail<TValue>(IError error)
        {
            var DomainResult = new DomainResult<TValue>();
            DomainResult.WithError(error);
            return DomainResult;
        }

        /// <summary>
        /// Creates a failed DomainResult with the given error message. Internally an error object from type Error is created. 
        /// </summary>
        public static DomainResult<TValue> Fail<TValue>(string errorMessage)
        {
            var DomainResult = new DomainResult<TValue>();
            DomainResult.WithError(new Error(errorMessage));
            return DomainResult;
        }

        /// <summary>
        /// Merge multiple DomainResult objects to one DomainResult object together
        /// </summary>
        public static DomainResult Merge(params DomainResult[] DomainResults)
        {
            return DomainResultHelper.Merge(DomainResults);
        }

        /// <summary>
        /// Merge multiple DomainResult objects to one DomainResult object together. Return one DomainResult with a list of merged values.
        /// </summary>
        public static DomainResult<IEnumerable<TValue>> Merge<TValue>(params DomainResult<TValue>[] DomainResults)
        {
            return DomainResultHelper.MergeWithValue(DomainResults);
        }

        /// <summary>
        /// Create a success/failed DomainResult depending on the parameter isSuccess
        /// </summary>
        public static DomainResult OkIf(bool isSuccess, IError error)
        {
            return isSuccess ? Ok() : Fail(error);
        }

        /// <summary>
        /// Create a success/failed DomainResult depending on the parameter isSuccess
        /// </summary>
        public static DomainResult OkIf(bool isSuccess, string error)
        {
            return isSuccess ? Ok() : Fail(error);
        }

        /// <summary>
        /// Create a success/failed DomainResult depending on the parameter isFailure
        /// </summary>
        public static DomainResult FailIf(bool isFailure, IError error)
        {
            return isFailure ? Fail(error) : Ok();
        }

        /// <summary>
        /// Create a success/failed DomainResult depending on the parameter isFailure
        /// </summary>
        public static DomainResult FailIf(bool isFailure, string error)
        {
            return isFailure ? Fail(error) : Ok();
        }

        /// <summary>
        /// Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object
        /// </summary>
        public static DomainResult Try(Action action, Func<Exception, IError> catchHandler = null)
        {
            catchHandler = catchHandler ?? Settings.DefaultTryCatchHandler;

            try
            {
                action();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object
        /// </summary>
        public static async Task<DomainResult> Try(Func<Task> action, Func<Exception, IError> catchHandler = null)
        {
            catchHandler = catchHandler ?? Settings.DefaultTryCatchHandler;

            try
            {
                await action();
                return Ok();
            }
            catch (Exception e)
            {
                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object
        /// </summary>
        public static DomainResult<T> Try<T>(Func<T> action, Func<Exception, IError> catchHandler = null)
        {
            catchHandler = catchHandler ?? Settings.DefaultTryCatchHandler;

            try
            {
                return Ok(action());
            }
            catch (Exception e)
            {
                return Fail(catchHandler(e));
            }
        }

        /// <summary>
        /// Executes the action. If an exception is thrown within the action then this exception is transformed via the catchHandler to an Error object
        /// </summary>
        public static async Task<DomainResult<T>> Try<T>(Func<Task<T>> action, Func<Exception, IError> catchHandler = null)
        {
            catchHandler = catchHandler ?? Settings.DefaultTryCatchHandler;

            try
            {
                return Ok(await action());
            }
            catch (Exception e)
            {
                return Fail(catchHandler(e));
            }
        }
    }
}
