using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public MiddlewareComponent(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
        }

        public IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> Middleware { get; }

        public IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> component;
            private readonly IComponentInvoker<TNextRequest, TNextResponse> next;

            public Invoker(MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> component, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                this.component = component;
                this.next = next;
            }

            public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                return component.Middleware.InvokeAsync(request, invocationContext, next);
            }
        }
    }
}