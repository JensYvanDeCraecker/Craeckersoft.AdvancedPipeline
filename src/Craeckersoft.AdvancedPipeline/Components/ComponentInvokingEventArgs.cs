using System;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class ComponentInvokingEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> : EventArgs
    {
        public ComponentInvokingEventArgs(TRequest request, IInvocationContext invocationContext, IComponentInvoker<TRequest, TResponse> current, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            Request = request;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public TRequest Request { get; set; }

        public IComponentInvoker<TRequest, TResponse> Current { get; }

        public IInvocationContext InvocationContext { get; }

        public IComponentInvoker<TNextRequest, TNextResponse> Next { get; }
    }
}