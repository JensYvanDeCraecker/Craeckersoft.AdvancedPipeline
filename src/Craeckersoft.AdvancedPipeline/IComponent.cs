namespace Craeckersoft.AdvancedPipeline
{
    public interface IComponent<in TRequest, out TNextRequest, in TNextResponse, out TResponse>
    {
        IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}