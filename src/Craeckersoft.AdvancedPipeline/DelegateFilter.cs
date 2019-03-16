using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public class DelegateFilter<TRequest, TResponse> : FilterBase<TRequest, TResponse>
    {
        public DelegateFilter(FilterDelegate<TRequest, TResponse> filterDelegate)
        {
            Delegate = filterDelegate ?? throw new ArgumentNullException(nameof(filterDelegate));
        }

        public FilterDelegate<TRequest, TResponse> Delegate { get; }

        protected override Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }
    }
}