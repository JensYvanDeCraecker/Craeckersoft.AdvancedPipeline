namespace Craeckersoft.AdvancedPipeline.Components
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate InvokerDelegate<TRequest, TResponse> ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(IInvoker<TNextRequest, TNextResponse> next);
}