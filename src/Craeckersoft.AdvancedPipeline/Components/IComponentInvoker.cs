using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public interface IComponentInvoker<in TRequest, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}