using Craeckersoft.AdvancedPipeline.Internal;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions
{
    public class MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse> : ReferenceTypeAssertions<IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>, MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public MiddlewareAssertions(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            Subject = middleware;
        }

        protected override string Identifier { get; } = "middleware";

        [CustomAssertion]
        public AndWhichConstraint<MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>> BeDelegateMiddleware(string because = "", params object[] becauseArgs)
        {
            return BeOfType<DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs);
        }
    }
}