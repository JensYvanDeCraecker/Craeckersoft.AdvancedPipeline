namespace Craeckersoft.AdvancedPipeline.Components
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate ComponentInvokerDelegate<TRequest, TResponse> ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(IComponentInvoker<TNextRequest, TNextResponse> next);
}