namespace Craeckersoft.AdvancedPipeline
{
    public delegate TResponse FilterDelegate<in TRequest, out TResponse>(TRequest request, IPipelineInvocationContext invocationContext);
}