using Craeckersoft.AdvancedPipeline.Internal;

namespace Craeckersoft.AdvancedPipeline
{
    public static class Filter
    {
        public static IFilter<TRequest, TResponse> FromDelegate<TRequest, TResponse>(FilterDelegate<TRequest, TResponse> filterDelegate)
        {
            return new DelegateFilter<TRequest, TResponse>(filterDelegate);
        }
    }
}