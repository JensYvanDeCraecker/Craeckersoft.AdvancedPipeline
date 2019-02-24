namespace Craeckersoft.AdvancedPipeline
{
    public delegate TResponse MiddlewareDelegate<in TRequest, out TNextRequest, in TNextResponse, out TResponse>(TRequest request, IPipelineInvocationContext invocationContext, ComponentInvokerDelegate<TNextRequest, TNextResponse> next);
}