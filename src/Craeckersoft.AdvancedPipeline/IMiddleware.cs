using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    // ReSharper disable once TypeParameterCanBeVariant
    public interface IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}