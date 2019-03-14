namespace Craeckersoft.AdvancedPipeline.Components
{
    public static class Component
    {
        public static DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> FromDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            return new DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse>(componentDelegate);
        }

        public static MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> FromMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            return new MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse>(middleware);
        }

        public static FilterComponent<TRequest, TFilterResponse, TResponse> FromFilter<TRequest, TFilterResponse, TResponse>(IFilter<TRequest, TFilterResponse> filter)
        {
            return new FilterComponent<TRequest, TFilterResponse, TResponse>(filter);
        }
    }
}