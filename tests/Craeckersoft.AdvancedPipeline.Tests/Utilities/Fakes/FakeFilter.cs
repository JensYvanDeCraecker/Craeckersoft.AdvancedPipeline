using System.Collections.Generic;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes
{
    public class FakeFilter : IFilter<object, object>
    {
        private readonly ISet<TestType> tests;

        public FakeFilter(ISet<TestType> tests)
        {
            this.tests = tests;
        }

        public Task<object> InvokeAsync(object request, IInvocationContext invocationContext)
        {
            tests?.Add(TestType.CurrentInvoker);
            if (request != null)
                tests?.Add(TestType.Request);
            if (invocationContext != null)
                tests?.Add(TestType.InvocationContext);
            return Task.FromResult(request);
        }
    }
}