using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class FilterComponent<TRequest, TFilterResponse, TResponse> : IComponent<TRequest, TFilterResponse, TResponse, TResponse>
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

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly FilterComponent<TRequest, TFilterResponse, TResponse> filterComponent;
            private readonly IComponentInvoker<TFilterResponse, TResponse> next;

            public Invoker(FilterComponent<TRequest, TFilterResponse, TResponse> filterComponent, IComponentInvoker<TFilterResponse, TResponse> next)
            {
                this.filterComponent = filterComponent;
                this.next = next;
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return next.Invoke(filterComponent.Filter.Invoke(request, invocationContext), invocationContext);
            }
        }
    }
}