namespace Craeckersoft.AdvancedPipeline.Components
{
    public delegate ComponentInvokerDelegate<TRequest, TResponse> ComponentDelegate<in TRequest, out TNextRequest, TNextResponse, TResponse>(IComponentInvoker<TNextRequest, TNextResponse> next);
}