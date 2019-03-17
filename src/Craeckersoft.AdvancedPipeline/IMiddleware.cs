using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public interface IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        event EventHandler<MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse>> Invoking;

        event EventHandler<MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse>> Invoked;

        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next);
    }
}