using Craeckersoft.AdvancedPipeline.Internal;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace Craeckersoft.AdvancedPipeline.Tests.Assertions
{
    public class FilterAssertions<TRequest, TResponse> : ReferenceTypeAssertions<IFilter<TRequest, TResponse>, FilterAssertions<TRequest, TResponse>>
    {
        public FilterAssertions(IFilter<TRequest, TResponse> filter)
        {
            Subject = filter;
        }

        protected override string Identifier { get; } = "filter";

        public AndWhichConstraint<FilterAssertions<TRequest, TResponse>, DelegateFilter<TRequest, TResponse>> BeDelegateFilter(string because = "", params object[] becauseArgs)
        {
            return new AndWhichConstraint<FilterAssertions<TRequest, TResponse>, DelegateFilter<TRequest, TResponse>>(this, BeOfType<DelegateFilter<TRequest, TResponse>>(because, becauseArgs).Which);
        }
    }
}