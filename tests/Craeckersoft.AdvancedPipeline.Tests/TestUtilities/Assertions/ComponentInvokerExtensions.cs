namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public static class ComponentInvokerExtensions
    {
        public static ComponentInvokerAssertions<TRequest, TResponse> Should<TRequest, TResponse>(this IInvoker<TRequest, TResponse> componentInvoker)
        {
            return new ComponentInvokerAssertions<TRequest, TResponse>(componentInvoker);
        }
    }
}