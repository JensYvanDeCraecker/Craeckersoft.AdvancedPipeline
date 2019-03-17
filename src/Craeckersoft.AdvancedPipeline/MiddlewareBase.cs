using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public abstract class MiddlewareBase<TRequest, TNextRequest, TNextResponse, TResponse> : IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public event EventHandler<MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse>> Invoking;

        public event EventHandler<MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse>> Invoked;

        public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next)
        {
            MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse> invokingEventArgs = new MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse>(request, invocationContext, next);
            OnInvoking(invokingEventArgs);
            TResponse response = await InvokeAsyncImpl(invokingEventArgs.Request, invocationContext, next);
            MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse> invokedEventArgs = new MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse>(response, invocationContext, next);
            OnInvoked(invokedEventArgs);
            return invokedEventArgs.Response;
        }

        protected virtual void OnInvoking(MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse> e)
        {
            Invoking?.Invoke(this, e);
        }

        protected virtual void OnInvoked(MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse> e)
        {
            Invoked?.Invoke(this, e);
        }

        protected abstract Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next);
    }
}