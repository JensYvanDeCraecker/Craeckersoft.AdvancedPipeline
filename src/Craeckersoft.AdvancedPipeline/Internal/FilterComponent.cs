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
            return new Invoker(Filter, next);
        }

        private class Invoker : IComponentInvoker<TRequest, TResponse>
        {
            private readonly IFilter<TRequest, TFilterResponse> filter;
            private readonly IComponentInvoker<TFilterResponse, TResponse> next;

            public Invoker(IFilter<TRequest, TFilterResponse> filter, IComponentInvoker<TFilterResponse, TResponse> next)
            {
                this.filter = filter;
                this.next = next;
            }

            public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
            {
                return next.Invoke(filter.Invoke(request, invocationContext), invocationContext);
            }
        }
    }
}