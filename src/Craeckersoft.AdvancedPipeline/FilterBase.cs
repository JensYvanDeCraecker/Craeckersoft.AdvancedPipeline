using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public abstract class FilterBase<TRequest, TResponse> : IFilter<TRequest, TResponse>
    {
        public event EventHandler<FilterInvokingEventArgs<TRequest>> Invoking;

        public event EventHandler<FilterInvokedEventArgs<TResponse>> Invoked;

        public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
        {
            FilterInvokingEventArgs<TRequest> invokingEventArgs = new FilterInvokingEventArgs<TRequest>(request, invocationContext);
            OnInvoking(invokingEventArgs);
            TResponse response = await InvokeAsyncImpl(invokingEventArgs.Request, invocationContext);
            FilterInvokedEventArgs<TResponse> invokedEventArgs = new FilterInvokedEventArgs<TResponse>(response, invocationContext);
            return invokedEventArgs.Response;
        }

        protected virtual void OnInvoking(FilterInvokingEventArgs<TRequest> e)
        {
            Invoking?.Invoke(this, e);
        }

        protected virtual void OnInvoked(FilterInvokedEventArgs<TResponse> e)
        {
            Invoked?.Invoke(this, e);
        }

        protected abstract Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext);
    }
}