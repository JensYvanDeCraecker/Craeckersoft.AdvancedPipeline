using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions
{
    public static class ComponentInvokerExtensions
    {
        public static ComponentInvokerAssertions<TRequest, TResponse> Should<TRequest, TResponse>(this IComponentInvoker<TRequest, TResponse> componentInvoker)
        {
            return new ComponentInvokerAssertions<TRequest, TResponse>(componentInvoker);
        }
    }
}