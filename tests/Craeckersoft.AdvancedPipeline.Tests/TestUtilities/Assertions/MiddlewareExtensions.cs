namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public static class MiddlewareExtensions
    {
        public static MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse> Should<TRequest, TNextRequest, TNextResponse, TResponse>(this IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            return new MiddlewareAssertions<TRequest, TNextRequest, TNextResponse, TResponse>(middleware);
        }
    }
}