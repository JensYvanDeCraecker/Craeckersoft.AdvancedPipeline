using System;

namespace Craeckersoft.AdvancedPipeline.Components
{
    public class ComponentInvokedEventArgs<TRequest, TNextRequest, TNextResponse, TResponse> : EventArgs
    {
        public ComponentInvokedEventArgs(TResponse response, IInvocationContext invocationContext, IComponentInvoker<TRequest, TResponse> current, IComponentInvoker<TNextRequest, TNextResponse> next)
        {
            Response = response;
            InvocationContext = invocationContext ?? throw new ArgumentNullException(nameof(invocationContext));
            Current = current ?? throw new ArgumentNullException(nameof(current));
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public TResponse Response { get; set; }

        public IComponentInvoker<TRequest, TResponse> Current { get; }

        public IInvocationContext InvocationContext { get; }

        public IComponentInvoker<TNextRequest, TNextResponse> Next { get; }
    }
}