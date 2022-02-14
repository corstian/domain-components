using Domain.Components.Abstractions;
using NSpecifications;
using System.Linq.Expressions;

namespace Domain.Components
{
    public static class AuthSpec
    {
    }

    public class AuthSpec<T> : ASpec<T>
    {
        private readonly Expression<Func<T, IAuthorizationContext, bool>> _expression;

        private readonly Lazy<Func<T, IAuthorizationContext, bool>> _compiledExpression;

        private readonly IAuthorizationContext _authorizationContext;

        public override Expression<Func<T, bool>> Expression => (t) => _compiledExpression.Value.Invoke(t, _authorizationContext);

        public AuthSpec(IAuthorizationContext context, Expression<Func<T, IAuthorizationContext, bool>> expression)
        {
            _expression = expression;
            _authorizationContext = context;
            _compiledExpression = new Lazy<Func<T, IAuthorizationContext, bool>>(() => _expression.Compile());
        }

        public override bool IsSatisfiedBy(T candidate)
        {
            return _compiledExpression.Value.Invoke(candidate, _authorizationContext);
        }
    }
}
