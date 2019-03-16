namespace Craeckersoft.AdvancedPipeline.Components
{
    public interface IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}