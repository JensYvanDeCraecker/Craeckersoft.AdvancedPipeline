using System;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Utilities.Internal
{
    public class ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> : IComponentInfo, IWrapper<IComponent<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        public ComponentInfo(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            Component = component ?? throw new ArgumentNullException(nameof(component));
        }

        public IComponent<TRequest, TNextRequest, TNextResponse, TResponse> Component { get; }

        public Type RequestType { get; } = typeof(TRequest);

        public Type ResponseType { get; } = typeof(TResponse);

        public Type NextRequestType { get; } = typeof(TNextRequest);

        public Type NextResponseType { get; } = typeof(TNextResponse);

        public object CreateInvoker(object next)
        {
            return Component.CreateInvoker((IComponentInvoker<TNextRequest, TNextResponse>)next);
        }

        object IWrapper.Item
        {
            get
            {
                return Component;
            }
        }

        IComponent<TRequest, TNextRequest, TNextResponse, TResponse> IWrapper<IComponent<TRequest, TNextRequest, TNextResponse, TResponse>>.Item
        {
            get
            {
                return Component;
            }
        }
    }
}