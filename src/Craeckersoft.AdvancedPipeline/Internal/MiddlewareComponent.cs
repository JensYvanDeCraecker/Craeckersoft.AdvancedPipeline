using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public MiddlewareComponent(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
        }

        public IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> Middleware { get; }

        public IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(Middleware, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware;
            private readonly IComponentInvoker<TNextRequest, TNextResponse> next;

            public Invoker(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                this.middleware = middleware;
                this.next = next;
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return middleware.Invoke(request, invocationContext, next);
            }
        }
    }
}