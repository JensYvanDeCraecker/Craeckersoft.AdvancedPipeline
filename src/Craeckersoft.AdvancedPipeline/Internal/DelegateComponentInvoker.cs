using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public class DelegateComponentInvoker<TRequest, TResponse> : IComponentInvoker<TRequest, TResponse>
    {
        public DelegateComponentInvoker(ComponentInvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            Delegate = componentInvokerDelegate ?? throw new ArgumentNullException(nameof(componentInvokerDelegate));
        }

        public ComponentInvokerDelegate<TRequest, TResponse> Delegate { get; }

        public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }
    }
}