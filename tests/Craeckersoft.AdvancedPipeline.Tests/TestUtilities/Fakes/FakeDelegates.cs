using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.TestUtilities.Fakes
{
    public static class FakeDelegates
    {
        public static ComponentDelegate<object, object, object, object> ComponentNull { get; } = next => null;

        public static ComponentDelegate<object, object, object, object> Component(ISet<TestItem> tests)
        {
            return next => (request, invocationContext) =>
            {
                tests?.Add(TestItem.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestItem.Request);
                if (invocationContext != null)
                    tests?.Add(TestItem.InvocationContext);
                if (next != null)
                    tests?.Add(TestItem.NextInvoker);
                return next?.InvokeAsync(request, invocationContext);
            };
        }

        public static MiddlewareDelegate<object, object, object, object> Middleware(ISet<TestItem> tests)
        {
            return (request, invocationContext, next) =>
            {
                tests?.Add(TestItem.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestItem.Request);
                if (invocationContext != null)
                    tests?.Add(TestItem.InvocationContext);
                if (next != null)
                    tests?.Add(TestItem.NextInvoker);
                return next?.InvokeAsync(request, invocationContext);
            };
        }

        public static FilterDelegate<object, object> Filter(ISet<TestItem> tests)
        {
            return (request, invocationContext) =>
            {
                tests?.Add(TestItem.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestItem.Request);
                if (invocationContext != null)
                    tests?.Add(TestItem.InvocationContext);
                return Task.FromResult(request);
            };
        }

        public static InvokerDelegate<object, object> ComponentInvoker(ISet<TestItem> tests)
        {
            return (request, invocationContext) =>
            {
                tests?.Add(TestItem.NextInvokerInvoked);
                return Task.FromResult(request);
            };
        }
    }
}