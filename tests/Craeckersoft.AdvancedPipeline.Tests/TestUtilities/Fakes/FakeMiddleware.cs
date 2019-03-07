using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public class FakeMiddleware : IMiddleware<object, object, object, object>

    {
        private readonly ISet<TestItem> tests;

        public FakeMiddleware(ISet<TestItem> tests)
        {
            this.tests = tests;
        }

        public Task<object> InvokeAsync(object request, IInvocationContext invocationContext, IComponentInvoker<object, object> next)
        {
            tests?.Add(TestItem.CurrentInvoker);
            if (request != null)
                tests?.Add(TestItem.Request);
            if (invocationContext != null)
                tests?.Add(TestItem.InvocationContext);
            if (next != null)
                tests?.Add(TestItem.NextInvoker);
            return next?.InvokeAsync(request, invocationContext);
        }
    }
}