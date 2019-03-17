using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IInvoker<TRequest, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}