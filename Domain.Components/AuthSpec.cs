using Domain.Components.Abstractions;
using NSpecifications;
using System.Linq.Expressions;

namespace Domain.Components
{
    public class AuthSpec<TAggregate> : ASpec<TAggregate>
        where TAggregate : IAggregate<TAggregate>
    {
        private readonly Expression<Func<TAggregate, IAuthorizationContext, bool>> _expression;

        private readonly Lazy<Func<TAggregate, IAuthorizationContext, bool>> _compiledExpression;

        private readonly IAuthorizationContext _authorizationContext;

        public override Expression<Func<TAggregate, bool>> Expression => (t) => _compiledExpression.Value.Invoke(t, _authorizationContext);

        public AuthSpec(
            IAuthorizationContext context,
            Expression<Func<TAggregate, IAuthorizationContext, bool>> expression)
        {
            _expression = expression;
            _authorizationContext = context;
            _compiledExpression = new Lazy<Func<TAggregate, IAuthorizationContext, bool>>(() => _expression.Compile());
        }

        public override bool IsSatisfiedBy(TAggregate candidate)
        {
            return _compiledExpression.Value.Invoke(candidate, _authorizationContext);
        }
    }

    public class AuthSpec<TAggregate, TContext> : ASpec<TAggregate>
        where TAggregate : IAggregate<TAggregate>
        where TContext : IAuthorizationContext
    {
        private readonly Expression<Func<TAggregate, TContext, bool>> _expression;

        private readonly Lazy<Func<TAggregate, TContext, bool>> _compiledExpression;

        private readonly TContext _authorizationContext;

        public override Expression<Func<TAggregate, bool>> Expression => (t) => _compiledExpression.Value.Invoke(t, _authorizationContext);

        public AuthSpec(
            TContext context, 
            Expression<Func<TAggregate, TContext, bool>> expression)
        {
            _expression = expression;
            _authorizationContext = context;
            _compiledExpression = new Lazy<Func<TAggregate, TContext, bool>>(() => _expression.Compile());
        }

        public override bool IsSatisfiedBy(TAggregate candidate)
        {
            return _compiledExpression.Value.Invoke(candidate, _authorizationContext);
        }
    }
}
