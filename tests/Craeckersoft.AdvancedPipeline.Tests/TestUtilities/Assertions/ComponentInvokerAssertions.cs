using Craeckersoft.AdvancedPipeline.Components;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public class ComponentInvokerAssertions<TRequest, TResponse> : ReferenceTypeAssertions<IComponentInvoker<TRequest, TResponse>, ComponentInvokerAssertions<TRequest, TResponse>>
    {
        public ComponentInvokerAssertions(IComponentInvoker<TRequest, TResponse> componentInvoker)
        {
            Subject = componentInvoker;
        }

        protected override string Identifier { get; } = "componentInvoker";

        [CustomAssertion]
        public AndWhichConstraint<ComponentInvokerAssertions<TRequest, TResponse>, DelegateComponentInvoker<TRequest, TResponse>> BeDelegateComponentInvoker(string because = "", params object[] becauseArgs)
        {
            return BeOfType<DelegateComponentInvoker<TRequest, TResponse>>(because, becauseArgs);
        }
    }
}