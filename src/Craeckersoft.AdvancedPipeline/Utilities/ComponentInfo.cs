using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Utilities.Internal;

namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public static class ComponentInfo
    {
        public static IComponentInfo From<TRequest, TNextRequest, TNextResponse, TResponse>(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            return new ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse>(component);
        }
    }
}