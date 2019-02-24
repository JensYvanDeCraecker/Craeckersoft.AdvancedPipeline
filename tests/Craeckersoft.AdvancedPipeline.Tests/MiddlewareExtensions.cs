using Craeckersoft.AdvancedPipeline.Tests.Assertions;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public static class MiddlewareExtensions
    {
        public static MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse> Should<TRequest, TNextRequest, TNextResponse, TResponse>(this IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            return new MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>(middleware);
        }
    }
}