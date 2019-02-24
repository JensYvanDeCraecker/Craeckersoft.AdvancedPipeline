using Craeckersoft.AdvancedPipeline.Internal;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.Assertions
{
    public class MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse> : ReferenceTypeAssertions<IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>, MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public MiddlewareAssertions(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            Subject = middleware;
        }

        protected override string Identifier { get; } = "middleware";

        public AndWhichConstraint<MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>> BeDelegateMiddleware(string because = "", params object[] becauseArgs)
        {
            return new AndWhichConstraint<MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>>(this, BeOfType<DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs).Which);
        }
    }
}