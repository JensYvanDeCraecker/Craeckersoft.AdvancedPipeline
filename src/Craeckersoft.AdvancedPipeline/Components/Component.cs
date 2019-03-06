using Craeckersoft.AdvancedPipeline.Components.Internal;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public static class Component
    {
        public static IComponent<TRequest, TNextRequest, TNextResponse, TResponse> FromDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            return new DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>(componentDelegate);
        }

        public static IComponent<TRequest, TNextRequest, TNextResponse, TResponse> FromMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            return new MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>(middleware);
        }

        public static IComponent<TRequest, TFilterResponse, TResponse, TResponse> FromFilter<TRequest, TFilterResponse, TResponse>(IFilter<TRequest, TFilterResponse> filter)
        {
            return new FilterComponent<TRequest, TFilterResponse, TResponse>(filter);
        }
    }
}