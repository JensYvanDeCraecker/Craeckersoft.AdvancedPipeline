using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IFilter<TRequest, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}