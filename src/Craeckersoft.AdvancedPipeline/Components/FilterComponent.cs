using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class FilterComponent<TRequest, TFilterResponse, TResponse> : ComponentBase<TRequest, TFilterResponse, TResponse, TResponse>
    {
        public FilterComponent(IFilter<TRequest, TFilterResponse> filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }

        public IFilter<TRequest, TFilterResponse> Filter { get; }

        protected sealed override async Task<TResponse> InvokeAsyncImpl(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TFilterResponse, TResponse> next)
        {
            return await next.InvokeAsync(await Filter.InvokeAsync(request, invocationContext), invocationContext);
        }
    }
}