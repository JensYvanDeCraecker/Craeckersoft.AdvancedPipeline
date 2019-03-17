namespace Craeckersoft.AdvancedPipeline.Components
{
    public interface IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        IInvoker<TRequest, TResponse> GetInvoker(IInvoker<TNextRequest, TNextResponse> next);
    }
}