using System;

namespace Craeckersoft.AdvancedPipeline
{
    public class FilterInvokedEventArgs<TResponse> : EventArgs
    {
        public FilterInvokedEventArgs(TResponse response, IInvocationContext invocationContext)
        {
            Response = response;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
        }

        public TResponse Response { get; set; }

        public IInvocationContext InvocationContext { get; }
    }
}