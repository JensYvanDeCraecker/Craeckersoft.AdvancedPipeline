namespace Craeckersoft.AdvancedPipeline
{
    public static class Invoker
    {
        public static DelegateInvoker<TRequest, TResponse> FromDelegate<TRequest, TResponse>(InvokerDelegate<TRequest, TResponse> componentInvokerDelegate)
        {
            return new DelegateInvoker<TRequest, TResponse>(componentInvokerDelegate);
        }
    }
}