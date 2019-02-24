using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> : IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateMiddleware(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            MiddlewareDelegate = middlewareDelegate ?? throw new ArgumentNullException(nameof(middlewareDelegate));
        }

        public MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> MiddlewareDelegate { get; }

        public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            return MiddlewareDelegate(request, invocationContext, next.Invoke);
        }
    }
}