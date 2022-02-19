using FluentResults;

namespace Domain.Components.Abstractions
{
    public interface IResult<out T>
    {
        public T ValueOrDefault { get; }
        public T Value { get; }

        public bool IsFailed { get; }
        public bool IsSuccess { get; }
        public List<IError> Errors { get; }
        public List<ISuccess> Successes { get; }
    }
}
