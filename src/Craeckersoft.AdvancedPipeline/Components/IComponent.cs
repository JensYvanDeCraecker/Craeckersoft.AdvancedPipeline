using System;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public interface IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        event EventHandler<ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoking;

        event EventHandler<ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoked;

        IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TNextRequest, TNextResponse> next);
    }
}