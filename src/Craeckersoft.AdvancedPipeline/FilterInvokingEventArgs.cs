using System;

namespace Craeckersoft.AdvancedPipeline
{
    public class FilterInvokingEventArgs<TRequest> : EventArgs
    {
        public FilterInvokingEventArgs(TRequest request, IInvocationContext invocationContext)
        {
            Request = request;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
        }

        public TRequest Request { get; set; }

        public IInvocationContext InvocationContext { get; }
    }
}