namespace Craeckersoft.AdvancedPipeline
{
    public static class Middleware
    {
        public static DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> FromDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            return new DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>(middlewareDelegate);
        }
    }
}