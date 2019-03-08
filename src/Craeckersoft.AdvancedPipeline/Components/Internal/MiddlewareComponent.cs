using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Components.Internal
{
    public sealed class MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>, IWrapper<IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>>
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

        IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> IWrapper<IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>>.Item
        {
            get
            {
                return Middleware;
            }
        }

        object IWrapper.Item
        {
            get
            {
                return Middleware;
            }
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

            public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                return middlewareComponent.Middleware.InvokeAsync(request, invocationContext, next);
            }
        }
    }
}