namespace Craeckersoft.AdvancedPipeline
{
    public delegate ComponentInvokerDelegate<TRequest, TResponse> ComponentDelegate<in TRequest, out TNextRequest, in TNextResponse, out TResponse>(ComponentInvokerDelegate<TNextRequest, TNextResponse> next);
}