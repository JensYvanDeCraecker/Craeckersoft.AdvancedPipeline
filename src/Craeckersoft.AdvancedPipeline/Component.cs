using Craeckersoft.AdvancedPipeline.Internal;

namespace Craeckersoft.AdvancedPipeline
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

        public static IComponent<TRequest, TNextRequest, TNextResponse, TResponse> FromMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            return FromMiddleware(new DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>(middlewareDelegate));
        }

        public static IComponent<TRequest, TFilterResponse, TResponse, TResponse> FromFilter<TRequest, TFilterResponse, TResponse>(IFilter<TRequest, TFilterResponse> filter)
        {
            return new FilterComponent<TRequest, TFilterResponse, TResponse>(filter);
        }

        public static IComponent<TRequest, TFilterResponse, TResponse, TResponse> FromFilter<TRequest, TFilterResponse, TResponse>(FilterDelegate<TRequest, TFilterResponse> filterDelegate)
        {
            return FromFilter<TRequest, TFilterResponse, TResponse>(new DelegateFilter<TRequest, TFilterResponse>(filterDelegate));
        }
    }
}