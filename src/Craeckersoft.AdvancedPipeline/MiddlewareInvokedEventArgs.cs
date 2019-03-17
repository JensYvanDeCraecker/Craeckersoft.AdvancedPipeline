using System;

namespace Craeckersoft.AdvancedPipeline
{
    public class MiddlewareInvokedEventArgs<TNextRequest, TNextResponse, TResponse> : EventArgs
    {
        public MiddlewareInvokedEventArgs(TResponse response, IInvocationContext invocationContext, IInvoker<TNextRequest, TNextResponse> next)
        {
            Response = response;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public TResponse Response { get; set; }

        public IInvocationContext InvocationContext { get; }

        public IInvoker<TNextRequest, TNextResponse> Next { get; }
    }
}