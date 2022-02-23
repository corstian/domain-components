using Domain.Components.Abstractions;
using FluentResults;

namespace Domain.Components
{
    public partial class DomainResult : ResultBase<DomainResult>
    {
        public DomainResult()
        { }

        public DomainResult<TNewValue> ToResult<TNewValue>(TNewValue newValue = default)
        {
            return new DomainResult<TNewValue>()
                .WithValue(IsFailed ? default : newValue)
                .WithReasons(Reasons);
        }
    }

    public class DomainResult<TValue> : ResultBase<DomainResult<TValue>>, IResult<TValue>
    {
        public DomainResult()
        { }

        private TValue _value;

        /// <summary>
        /// Get the Value. If result is failed then a default value is returned. Opposite see property Value.
        /// </summary>
        public TValue ValueOrDefault => _value;

        /// <summary>
        /// Get the Value. If result is failed then an Exception is thrown because a failed result has no value. Opposite see property ValueOrDefault.
        /// </summary>
        public TValue Value
        {
            get
            {
                if (IsFailed)
                    throw new InvalidOperationException("DomainResult is in status failed. Value is not set.");

                return _value;
            }
            private set
            {
                if (IsFailed)
                    throw new InvalidOperationException("DomainResult is in status failed. Value is not set.");

                _value = value;
            }
        }

        /// <summary>
        /// Set value
        /// </summary>
        public DomainResult<TValue> WithValue(TValue value)
        {
            Value = value;
            return this;
        }

        /// <summary>
        /// Convert result with value to result without value
        /// </summary>
        public DomainResult ToResult()
        {
            return new DomainResult()
                .WithReasons(Reasons);
        }

        /// <summary>
        /// Convert result with value to result with another value. Use valueConverter parameter to specify the value transformation logic.
        /// </summary>
        public DomainResult<TNewValue> ToResult<TNewValue>(Func<TValue, TNewValue> valueConverter = null)
        {
            if (IsSuccess && valueConverter == null)
                throw new ArgumentException("If result is success then valueConverter should not be null");

            return new DomainResult<TNewValue>()
                .WithValue(IsFailed ? default : valueConverter(Value))
                .WithReasons(Reasons);
        }

        public static implicit operator DomainResult<TValue>(DomainResult result)
        {
            return result.ToResult<TValue>();
        }
    }
}
