namespace Craeckersoft.AdvancedPipeline
{
    public delegate TResponse ComponentInvokerDelegate<in TRequest, out TResponse>(TRequest request, IPipelineInvocationContext invocationContext);
}