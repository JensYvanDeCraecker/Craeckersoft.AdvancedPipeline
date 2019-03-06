using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public delegate Task<TResponse> ComponentInvokerDelegate<in TRequest, TResponse>(TRequest request, IInvocationContext invocationContext);
}