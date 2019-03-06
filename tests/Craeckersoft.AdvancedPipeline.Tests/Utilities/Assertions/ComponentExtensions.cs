using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Assertions
{
    public static class ComponentExtensions
    {
        public static ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse> Should<TRequest, TNextRequest, TNextResponse, TResponse>(this IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            return new ComponentAssertions<TRequest, TNextRequest, TNextResponse, TResponse>(component);
        }
    }
}