namespace Craeckersoft.AdvancedPipeline
{
    public interface IComponentInvoker<in TRequest, out TResponse>
    {
        TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext);
    }
}