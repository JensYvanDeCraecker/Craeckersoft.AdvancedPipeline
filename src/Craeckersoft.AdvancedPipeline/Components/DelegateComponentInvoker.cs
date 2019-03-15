using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class DelegateComponentInvoker<TRequest, TResponse> : IComponentInvoker<TRequest, TResponse>
    {
        public DelegateComponentInvoker(ComponentInvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            Delegate = componentInvokerDelegate ?? throw new ArgumentNullException(nameof(componentInvokerDelegate));
        }

        public ComponentInvokerDelegate<TRequest, TResponse> Delegate { get; }

        public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }
    }
}