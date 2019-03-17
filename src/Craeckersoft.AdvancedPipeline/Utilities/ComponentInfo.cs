using System;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Utilities
{
    public static class ComponentInfo
    {
        public static ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> From<TRequest, TNextRequest, TNextResponse, TResponse>(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
        {
            return new ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse>(component);
        }
    }

    public sealed class ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> : IComponentInfo, IEquatable<ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse>>
    {
        internal ComponentInfo(IComponent<TRequest, TNextRequest, TNextResponse, TResponse> component)
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
            return Component.GetInvoker((IInvoker<TNextRequest, TNextResponse>)next);
        }

        public bool Equals(IComponentInfo other)
        {
            return other is ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> info && Equals(info);
        }

        public bool Equals(ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> other)
        {
            return other != null && Equals(Component, other.Component);
        }

        public override bool Equals(object obj)
        {
            return obj is ComponentInfo<TRequest, TNextRequest, TNextResponse, TResponse> info && Equals(info);
        }

        public override int GetHashCode()
        {
            return Component != null ? Component.GetHashCode() : 0;
        }
    }
}