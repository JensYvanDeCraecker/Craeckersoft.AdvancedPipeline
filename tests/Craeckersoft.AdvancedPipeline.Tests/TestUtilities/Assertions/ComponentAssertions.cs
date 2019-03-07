using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Components.Internal;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public class ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse> : ReferenceTypeAssertions<IComponent<TRequest, TNextRequest, TNextResponse, TResponse>, ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public ComponentAssertions(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            Subject = component;
        }

        protected override string Identifier { get; } = "component";

        [CustomAssertion]
        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>> BeMiddlewareComponent(string because = "", params object[] becauseArgs)
        {
            return BeOfType<MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs);
        }

        [CustomAssertion]
        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, FilterComponent<TRequest, TNextRequest, TResponse>> BeFilterComponent(string because = "", params object[] becauseArgs)
        {
            return BeOfType<FilterComponent<TRequest, TNextRequest, TResponse>>(because, becauseArgs);
        }

        [CustomAssertion]
        public AndWhichConstraint<ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>, DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>> BeDelegateComponent(string because = "", params object[] becauseArgs)
        {
            return BeOfType<DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>>(because, becauseArgs);
        }
    }
}