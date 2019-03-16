using System.Collections.Generic;
using System.Threading.Tasks;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public class FakeFilter : FilterBase<object, object>
    {
        private readonly ISet<TestItem> tests;

        public FakeFilter(ISet<TestItem> tests)
        {
            this.tests = tests;
        }

        protected override Task<object> InvokeAsyncImpl(object request, IInvocationContext invocationContext)
        {
            tests?.Add(TestItem.CurrentInvoker);
            if (request != null)
                tests?.Add(TestItem.Request);
            if (invocationContext != null)
                tests?.Add(TestItem.InvocationContext);
            return Task.FromResult(request);
        }
    }
}