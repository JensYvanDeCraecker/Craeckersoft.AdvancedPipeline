using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Components.Internal
{
    public sealed class FilterComponent<TRequest, TFilterResponse, TResponse> : IComponent<TRequest, TFilterResponse, TResponse, TResponse>, IWrapper<IFilter<TRequest, TFilterResponse>>
    {
        public FilterComponent(IFilter<TRequest, TFilterResponse> filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public IFilter<TRequest, TFilterResponse> Filter { get; }

        public IComponentInvoker<TRequest, TResponse> CreateInvoker(IComponentInvoker<TFilterResponse, TResponse> next)
        {
            if (next == null)
                throw new ArgumentNullException(nameof(next));
            return new Invoker(this, next);
        }

        IFilter<TRequest, TFilterResponse> IWrapper<IFilter<TRequest, TFilterResponse>>.Item
        {
            get
            {
                return Filter;
            }
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly FilterComponent<TRequest, TFilterResponse, TResponse> filterComponent;
            private readonly IComponentInvoker<TFilterResponse, TResponse> next;

            public Invoker(FilterComponent<TRequest, TFilterResponse, TResponse> filterComponent, IComponentInvoker<TFilterResponse, TResponse> next)
            {
                this.filterComponent = filterComponent;
                this.next = next;
            }

            public async Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
            {
                return await next.InvokeAsync(await filterComponent.Filter.InvokeAsync(request, invocationContext), invocationContext);
            }
        }
    }
}