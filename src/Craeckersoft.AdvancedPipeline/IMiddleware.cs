using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    public interface IMiddleware<in TRequest, out TNextRequest, TNextResponse, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}