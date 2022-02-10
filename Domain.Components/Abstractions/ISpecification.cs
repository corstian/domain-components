using System.Linq.Expressions;

namespace Domain.Components.Abstractions
{
    public interface ISpecification
    {
        Expression<Func<object, bool>> Evaluation { get; }
    }

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Evaluation { get; }
    }
}
