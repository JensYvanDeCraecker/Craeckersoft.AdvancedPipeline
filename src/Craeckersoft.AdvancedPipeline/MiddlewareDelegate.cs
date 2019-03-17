using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate Task<TResponse> MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse>(TRequest request, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next);
}