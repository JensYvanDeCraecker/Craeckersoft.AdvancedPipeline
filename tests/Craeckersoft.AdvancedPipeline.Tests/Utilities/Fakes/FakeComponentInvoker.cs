using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes
{
    public class FakeComponentInvoker : IComponentInvoker<object, object>
    {
        private readonly ISet<TestType> tests;

        public FakeComponentInvoker(ISet<TestType> tests)
        {
            this.tests = tests;
        }

        public Task<object> InvokeAsync(object request, IInvocationContext invocationContext)
        {
            tests?.Add(TestType.NextInvokerInvoked);
            return Task.FromResult(request);
        }
    }
}