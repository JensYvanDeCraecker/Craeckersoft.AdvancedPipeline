using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    public delegate Task<TResponse> MiddlewareDelegate<in TRequest, out TNextRequest, TNextResponse, TResponse>(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
}