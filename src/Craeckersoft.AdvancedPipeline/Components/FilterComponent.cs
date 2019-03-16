using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class FilterComponent<TRequest, TFilterResponse, TResponse> : IComponent<TRequest, TFilterResponse, TResponse, TResponse>
    {
        public FilterComponent(IFilter<TRequest, TFilterResponse> filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public IFilter<TRequest, TFilterResponse> Filter { get; }

        public IComponentInvoker<TRequest, TResponse> GetInvoker(IComponentInvoker<TFilterResponse, TResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly FilterComponent<TRequest, TFilterResponse, TResponse> component;
            private readonly IComponentInvoker<TFilterResponse, TResponse> next;

            public Invoker(FilterComponent<TRequest, TFilterResponse, TResponse> component, IComponentInvoker<TFilterResponse, TResponse> next)
            {
                this.component = component;
                this.next = next;
            }

            public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                return await next.InvokeAsync(await component.Filter.InvokeAsync(request, invocationContext), invocationContext);
            }
        }
    }
}