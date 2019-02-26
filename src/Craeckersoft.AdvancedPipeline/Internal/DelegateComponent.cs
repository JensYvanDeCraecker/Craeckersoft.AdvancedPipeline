using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateComponent(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            Delegate = componentDelegate ?? throw new ArgumentNullException(nameof(componentDelegate));
        }

        public ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        public IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly ComponentInvokerDelegate<TRequest, TResponse> componentInvoker;

            public Invoker(DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> delegateComponent, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                componentInvoker = delegateComponent.Delegate(next.Invoke) ?? throw new InvalidOperationException();
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return componentInvoker(request, invocationContext);
            }
        }
    }
}