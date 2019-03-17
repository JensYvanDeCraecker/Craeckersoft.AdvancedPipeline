using System;

namespace Craeckersoft.AdvancedPipeline
{
    public class MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse> : EventArgs
    {
        public MiddlewareInvokingEventArgs(TRequest request, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next)
        {
            Request = request;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public TRequest Request { get; set; }

        public IInvocationContext InvocationContext { get; }

        public IInvoker<TNextRequest, TNextResponse> Next { get; }
    }
}