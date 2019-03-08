using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateMiddleware<TRequest, TNextRequest, TNextResponse, TResponse> : IMiddleware<TRequest, TNextRequest, TNextResponse, TResponse>, IWrapper<MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public DelegateMiddleware(MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> middlewareDelegate)
        {
            Delegate = middlewareDelegate ?? throw new ArgumentNullException(nameof(middlewareDelegate));
        }

        public MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            return Delegate(request, invocationContext, next);
        }

        MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse> IWrapper<MiddlewareDelegate<TRequest, TNextRequest, TNextResponse, TResponse>>.Item
        {
            get
            {
                return Delegate;
            }
        }

        object IWrapper.Item
        {
            get
            {
                return Delegate;
            }
        }
    }
}