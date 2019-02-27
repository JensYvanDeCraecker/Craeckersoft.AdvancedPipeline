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
            return ComponentInvoker.FromDelegate(Delegate(next.Invoke) ?? throw new InvalidOperationException());
        }
    }
}