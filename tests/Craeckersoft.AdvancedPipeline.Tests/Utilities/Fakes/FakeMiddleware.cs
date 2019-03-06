using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes
{
    public class FakeMiddleware : IMiddleware<object, object, object, object>

    {
        private readonly ISet<TestType> tests;

        public FakeMiddleware(ISet<TestType> tests)
        {
            this.tests = tests;
        }

        public Task<object> InvokeAsync(object request, IInvocationContext invocationContext, IComponentInvoker<object, object> next)
        {
            tests?.Add(TestType.CurrentInvoker);
            if (request != null)
                tests?.Add(TestType.Request);
            if (invocationContext != null)
                tests?.Add(TestType.InvocationContext);
            if (next != null)
                tests?.Add(TestType.NextInvoker);
            return next?.InvokeAsync(request, invocationContext);
        }
    }
}