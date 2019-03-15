using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> : IComponent<TRequest, TNextRequest, TNextResponse, TResponse>
    {
        public DelegateComponent(ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> componentDelegate)
        {
            Delegate = componentDelegate ?? throw new ArgumentNullException(nameof(componentDelegate));
        }

        public ComponentDelegate<TRequest, TNextRequest, TNextResponse, TResponse> Delegate { get; }

        public event EventHandler<ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoking;

        public event EventHandler<ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>> Invoked;

        public IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TNextRequest, TNextResponse> next)
        {
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

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> component;
            private readonly ComponentInvokerDelegate<TRequest, TResponse> current;
            private readonly IComponentInvoker<TNextRequest, TNextResponse> next;

            public Invoker(DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> component, IComponentInvoker<TNextRequest, TNextResponse> next)
            {
                this.component = component;
                this.next = next;
                current = component.Delegate(this.next) ?? throw new InvalidOperationException();
            }

            public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> componentInvokingEventArgs = new ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>(request, invocationContext, this, next);
                component.OnInvoking(componentInvokingEventArgs);
                TResponse response = await current.Invoke(componentInvokingEventArgs.Request, invocationContext);
                ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> componentInvokedEventArgs = new ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse>(response, invocationContext, this, next);
                component.OnInvoked(componentInvokedEventArgs);
                return componentInvokedEventArgs.Response;
            }
        }
    }
}