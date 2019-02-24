namespace Craeckersoft.AdvancedPipeline
{
    public interface IFilter<TRequest, TResponse>
    {
        TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext);
    }
}