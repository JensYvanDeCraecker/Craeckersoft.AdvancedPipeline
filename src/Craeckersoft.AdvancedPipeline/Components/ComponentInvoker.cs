using Craeckersoft.AdvancedPipeline.Components.Internal;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public static class ComponentInvoker
    {
        public static IComponentInvoker<TRequest, TResponse> FromDelegate<TRequest, TResponse>(ComponentInvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            return new DelegateComponentInvoker<TRequest, TResponse>(componentInvokerDelegate);
        }
    }
}