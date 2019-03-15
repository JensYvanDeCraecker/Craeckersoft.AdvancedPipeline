using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate Task<TResponse> MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
}