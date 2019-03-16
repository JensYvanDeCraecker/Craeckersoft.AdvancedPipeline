using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    public interface IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        event EventHandler<MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse>> Invoking;

        event EventHandler<MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse>> Invoked;

        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}