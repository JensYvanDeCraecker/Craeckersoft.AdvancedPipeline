using System;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateFilter<TRequest, TResponse> : IFilter<TRequest, TResponse>
    {
        public DelegateFilter(FilterDelegate<TRequest, TResponse> filterDelegate)
        {
            Delegate = filterDelegate ?? throw new ArgumentNullException(nameof(filterDelegate));
        }

        public FilterDelegate<TRequest, TResponse> Delegate { get; }

        public TResponse Invoke(TRequest request, IPipelineInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }
    }
}