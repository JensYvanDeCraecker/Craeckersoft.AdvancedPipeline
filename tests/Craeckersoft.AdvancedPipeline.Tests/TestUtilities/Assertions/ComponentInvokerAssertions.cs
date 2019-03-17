using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public class ComponentInvokerAssertions<TRequest, TResponse> : ReferenceTypeAssertions<IInvoker<TRequest, TResponse>, ComponentInvokerAssertions<TRequest, TResponse>>
    {
        public ComponentInvokerAssertions(IInvoker<TRequest, TResponse> componentInvoker)
        {
            Subject = componentInvoker;
        }

        protected override string Identifier { get; } = "componentInvoker";

        [CustomAssertion]
        public AndWhichConstraint<ComponentInvokerAssertions<TRequest, TResponse>, DelegateInvoker<TRequest, TResponse>> BeDelegateComponentInvoker(string because = "", params object[] becauseArgs)
        {
            return BeOfType<DelegateInvoker<TRequest, TResponse>>(because, becauseArgs);
        }
    }
}