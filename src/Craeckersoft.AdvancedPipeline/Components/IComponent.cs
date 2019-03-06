namespace Craeckersoft.AdvancedPipeline.Components
{
    public interface IComponent<in TRequest, out TNextRequest, TNextResponse, TResponse>
    {
        IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}