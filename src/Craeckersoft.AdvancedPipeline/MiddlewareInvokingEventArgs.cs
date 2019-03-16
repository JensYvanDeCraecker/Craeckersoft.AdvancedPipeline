using System;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline
{
    public class MiddlewareInvokingEventArgs<TRequest, TNextRequest, TNextResponse> : EventArgs
    {
        public MiddlewareInvokingEventArgs(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            Request = request;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public TRequest Request { get; set; }

        public IInvocationContext InvocationContext { get; }

        public IComponentInvoker<TNextRequest, TNextResponse> Next { get; }
    }
}