using Craeckersoft.AdvancedPipeline.Internal;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.Assertions
{
    public class ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse> : ReferenceTypeAssertions<IComponent<TRequest, TNextRequest, TNextResponse, TResponse>, ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public ComponentAssertions(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            Subject = component;
        }

        protected override string Identifier { get; } = "component";

        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>> BeMiddlewareComponent(string because = "", params object[] becauseArgs)
        {
            return new AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(this, BeOfType<MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs).Which);
        }

        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, FilterComponent<TRequest, TNextRequest, TResponse>> BeFilterComponent(string because = "", params object[] becauseArgs)
        {
            return new AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, FilterComponent<TRequest, TNextRequest, TResponse>>(this, BeOfType<FilterComponent<TRequest, TNextRequest, TResponse>>(because, becauseArgs).Which);
        }

        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>> BeDelegateComponent(string because = "", params object[] becauseArgs)
        {
            return new AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(this, BeOfType<DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs).Which);
        }
    }
}