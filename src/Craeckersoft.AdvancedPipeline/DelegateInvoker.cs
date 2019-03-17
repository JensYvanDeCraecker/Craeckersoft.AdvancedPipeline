using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public class DelegateInvoker<TRequest, TResponse> : IInvoker<TRequest, TResponse>
    {
        public DelegateInvoker(InvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            Delegate = componentInvokerDelegate ?? throw new ArgumentNullException(nameof(componentInvokerDelegate));
        }

        public InvokerDelegate<TRequest, TResponse> Delegate { get; }

        public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }
    }
}