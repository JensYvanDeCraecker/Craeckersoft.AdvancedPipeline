using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public static class ComponentInfoExtensions
    {
        public static ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> GetInfo<TRequest, TNextRequest, TNextResponse, TResponse>(this IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            return ComponentInfo.From(component);
        }
    }
}