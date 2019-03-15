using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public abstract class ComponentBase<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public event EventHandler<ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoking;

        public event EventHandler<ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoked;

        public IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        protected virtual void OnInvoking(ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> e)
        {
            Invoking?.Invoke(this, e);
        }

        protected virtual void OnInvoked(ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> e)
        {
            Invoked?.Invoke(this, e);
        }

        protected abstract Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next);

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly ComponentBase<TRequest, TNextRequest, TNextResponse, TResponse> component;
            private readonly IComponentInvoker<TNextRequest, TNextResponse> next;

            public Invoker(ComponentBase<TRequest, TNextRequest, TNextResponse, TResponse> component, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                this.component = component;
                this.next = next;
            }

            public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> componentInvokingEventArgs = new ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>(request, invocationContext, this, next);
                component.OnInvoking(componentInvokingEventArgs);
                TResponse response = await component.InvokeAsync(componentInvokingEventArgs.Request, invocationContext, next);
                ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> componentInvokedEventArgs = new ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>(response, invocationContext, this, next);
                component.OnInvoked(componentInvokedEventArgs);
                return componentInvokedEventArgs.Response;
            }
        }
    }
}