using System;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Utilities;

namespace Craeckersoft.AdvancedPipeline.Internal
{
    public sealed class DelegateFilter<TRequest, TResponse> : IFilter<TRequest, TResponse>, IWrapper<FilterDelegate<TRequest, TResponse>>
    {
        public DelegateFilter(FilterDelegate<TRequest, TResponse> filterDelegate)
        {
            Delegate = filterDelegate ?? throw new ArgumentNullException(nameof(filterDelegate));
        }

        public FilterDelegate<TRequest, TResponse> Delegate { get; }

        public Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext)
        {
            return Delegate(request, invocationContext);
        }

        FilterDelegate<TRequest, TResponse> IWrapper<FilterDelegate<TRequest, TResponse>>.Item
        {
            get
            {
                return Delegate;
            }
        }

        object IWrapper.Item
        {
            get
            {
                return Delegate;
            }
        }
    }
}