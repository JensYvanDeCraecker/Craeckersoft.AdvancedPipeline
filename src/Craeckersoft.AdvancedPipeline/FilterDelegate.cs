using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public delegate Task<TResponse> FilterDelegate<in TRequest, TResponse>(TRequest request, IInvocationContext invocationContext);
}