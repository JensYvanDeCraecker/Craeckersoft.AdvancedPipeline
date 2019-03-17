using System.Collections.Generic;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public class FakeComponentInvoker : IInvoker<object, object>
    {
        private readonly ISet<TestItem> tests;

        public FakeComponentInvoker(ISet<TestItem> tests)
        {
            this.tests = tests;
        }

        public Task<object> InvokeAsync(object request, IInvocationContext invocationContext)
        {
            tests?.Add(TestItem.NextInvokerInvoked);
            return Task.FromResult(request);
        }
    }
}