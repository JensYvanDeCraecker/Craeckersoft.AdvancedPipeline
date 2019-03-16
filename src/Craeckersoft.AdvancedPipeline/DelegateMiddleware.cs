using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    public class DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> : MiddlewareBase<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateMiddleware(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            Delegate = middlewareDelegate ?? throw new ArgumentNullException(nameof(middlewareDelegate));
        }

        public MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        protected override Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            return Delegate(request, invocationContext, next);
        }
    }
}