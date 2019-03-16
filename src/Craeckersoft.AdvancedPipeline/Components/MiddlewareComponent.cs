using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class MiddlewareComponent<TRequest, TNextRequest, TNextResponse, TResponse> : ComponentBase<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public MiddlewareComponent(IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> middleware)
        {
            Middleware = middleware ?? throw new ArgumentNullException(nameof(middleware));
        }

        public IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> Middleware { get; }

        protected sealed override Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            return Middleware.InvokeAsync(request, invocationContext, next);
        }
    }
}