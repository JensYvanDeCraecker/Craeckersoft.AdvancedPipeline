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
            return new Invoker(this, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> middlewareComponent;
            private readonly IComponentInvoker<TNextRequest, TNextResponse> next;

            public Invoker(MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> middlewareComponent, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                this.middlewareComponent = middlewareComponent;
                this.next = next;
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return middlewareComponent.Middleware.Invoke(request, invocationContext, next);
            }
        }
    }
}