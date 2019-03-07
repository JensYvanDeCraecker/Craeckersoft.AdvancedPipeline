using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public class FakeComponentInvoker : IComponentInvoker<object, object>
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