namespace Craeckersoft.AdvancedPipeline
{
    public interface IMiddleware<in TRequest, out TNextRequest, in TNextResponse, out TResponse>
    {
        TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}