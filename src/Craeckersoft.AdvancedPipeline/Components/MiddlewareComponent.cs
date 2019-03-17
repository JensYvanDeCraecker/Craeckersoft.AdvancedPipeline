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

        public IInvoker<TRequest, TResponse> GetInvoker(IInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        private class Invoker : IInvoker<TRequest, TResponse>
        {
            private readonly MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> component;
            private readonly IInvoker<TNextRequest, TNextResponse> next;

            public Invoker(MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> component, IInvoker<TNextRequest, TNextResponse> next)
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