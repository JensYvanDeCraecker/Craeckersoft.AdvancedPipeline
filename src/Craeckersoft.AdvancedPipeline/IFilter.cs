using System;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline
{
    public interface IFilter<TRequest, TResponse>
    {
        event EventHandler<FilterInvokingEventArgs<TRequest>> Invoking;

        event EventHandler<FilterInvokedEventArgs<TResponse>> Invoked;

        Task<TResponse> InvokeAsync(TRequest request, IInvocationContext invocationContext);
    }
}