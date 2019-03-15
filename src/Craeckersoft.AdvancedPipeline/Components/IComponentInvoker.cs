using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IComponentInvoker<TRequest, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}