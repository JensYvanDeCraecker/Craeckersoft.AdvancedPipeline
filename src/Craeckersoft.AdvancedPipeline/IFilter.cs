using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public interface IFilter<in TRequest, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}