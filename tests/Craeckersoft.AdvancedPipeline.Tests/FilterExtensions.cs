using Craeckersoft.AdvancedPipeline.Tests.Assertions;

namespace Craeckersoft.AdvancedPipeline.Tests
{
    public static class FilterExtensions
    {
        public static FilterAssertions<TRequest, TResponse> Should<TRequest, TResponse>(this IFilter<TRequest, TResponse> filter)
        {
            return new FilterAssertions<TRequest, TResponse>(filter);
        }
    }
}