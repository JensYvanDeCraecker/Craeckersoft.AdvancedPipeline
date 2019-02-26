using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> : IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateMiddleware(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            Delegate = middlewareDelegate ?? throw new ArgumentNullException(nameof(middlewareDelegate));
        }

        public MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return Delegate(request, invocationContext, next.Invoke);
        }
    }
}