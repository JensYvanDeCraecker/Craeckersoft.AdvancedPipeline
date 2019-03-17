using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public delegate Task<TResponse> InvokerDelegate<in TRequest, TResponse>(TRequest request, IInvocationContext invocationContext);
}