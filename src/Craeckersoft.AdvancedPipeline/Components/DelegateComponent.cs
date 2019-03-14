using System;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public sealed class DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>, IWrapper<ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        internal DelegateComponent(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            Delegate = componentDelegate ?? throw new ArgumentNullException(nameof(componentDelegate));
        }

        public ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        public IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            return ComponentInvoker.FromDelegate(Delegate(next) ?? throw new InvalidOperationException());
        }

        ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> IWrapper<ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse>>.Item
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