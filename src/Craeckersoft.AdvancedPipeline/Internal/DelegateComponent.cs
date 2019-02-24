using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateComponent(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            ComponentDelegate = componentDelegate ?? throw new ArgumentNullException(nameof(componentDelegate));
        }

        public ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> ComponentDelegate { get; }

        public IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(ComponentDelegate(next.Invoke) ?? throw new InvalidOperationException());
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly ComponentInvokerDelegate<TRequest, TResponse> componentInvoker;

            public Invoker(ComponentInvokerDelegate<TRequest, TResponse> componentInvoker)
            {
                this.componentInvoker = componentInvoker;
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return componentInvoker(request, invocationContext);
            }
        }
    }
}