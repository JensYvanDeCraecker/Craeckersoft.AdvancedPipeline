namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Assertions
{
    public static class FilterExtensions
    {
        public static FilterAssertions<TRequest, TResponse> Should<TRequest, TResponse>(this IFilter<TRequest, TResponse> filter)
        {
            return new FilterAssertions<TRequest, TResponse>(filter);
        }
    }
}