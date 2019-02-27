namespace Craeckersoft.AdvancedPipeline
{
    public interface IFilter<in TRequest, out TResponse>
    {
        TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext);
    }
}