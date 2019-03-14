using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public sealed class DelegateComponentInvoker<TRequest, TResponse> : IComponentInvoker<TRequest, TResponse>, IWrapper<ComponentInvokerDelegate<TRequest, TResponse>>
    {
        internal DelegateComponentInvoker(ComponentInvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            Delegate = componentInvokerDelegate ?? throw new ArgumentNullException(nameof(componentInvokerDelegate));
        }

        public ComponentInvokerDelegate<TRequest, TResponse> Delegate { get; }

        public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }

        ComponentInvokerDelegate<TRequest, TResponse> IWrapper<ComponentInvokerDelegate<TRequest, TResponse>>.Item
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