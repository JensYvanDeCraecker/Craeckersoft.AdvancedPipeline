using System;

namespace Craeckersoft.AdvancedPipeline.Components.Internal
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
            return ComponentInvoker.FromDelegate(Delegate(next) ?? throw new InvalidOperationException());
        }
    }
}