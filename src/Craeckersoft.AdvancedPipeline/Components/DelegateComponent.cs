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

        public IInvoker<TRequest, TResponse> GetInvoker(IInvoker<TNextRequest, TNextResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        private class Invoker : IInvoker<TRequest, TResponse>
        {
            private readonly DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> component;
            private readonly InvokerDelegate<TRequest, TResponse> current;

            public Invoker(DelegateComponent<TRequest, TNextRequest, TNextResponse, TResponse> component, IInvoker<TNextRequest, TNextResponse> next)
            {
                this.component = component;
                current = component.Delegate(next) ?? throw new InvalidOperationException();
            }

            public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                return current.Invoke(request, invocationContext);
            }
        }
    }
}