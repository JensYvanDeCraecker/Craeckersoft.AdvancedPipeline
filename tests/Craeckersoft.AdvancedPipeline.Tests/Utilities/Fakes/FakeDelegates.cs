using System.Collections.Generic;
using System.Threading.Tasks;
using Craeckersoft.AdvancedPipeline.Components;

namespace Craeckersoft.AdvancedPipeline.Tests.Utilities.Fakes
{
    public static class FakeDelegates
    {
        public static ComponentDelegate<object, object, object, object> ComponentNull { get; } = next => null;

        public static ComponentDelegate<object, object, object, object> Component(ISet<TestType> tests)
        {
            return next => (request, invocationContext) =>
            {
                tests?.Add(TestType.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestType.Request);
                if (invocationContext != null)
                    tests?.Add(TestType.InvocationContext);
                if (next != null)
                    tests?.Add(TestType.NextInvoker);
                return next?.InvokeAsync(request, invocationContext);
            };
        }

        public static MiddlewareDelegate<object, object, object, object> Middleware(ISet<TestType> tests)
        {
            return (request, invocationContext, next) =>
            {
                tests?.Add(TestType.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestType.Request);
                if (invocationContext != null)
                    tests?.Add(TestType.InvocationContext);
                if (next != null)
                    tests?.Add(TestType.NextInvoker);
                return next?.InvokeAsync(request, invocationContext);
            };
        }

        public static FilterDelegate<object, object> Filter(ISet<TestType> tests)
        {
            return (request, invocationContext) =>
            {
                tests?.Add(TestType.CurrentInvoker);
                if (request != null)
                    tests?.Add(TestType.Request);
                if (invocationContext != null)
                    tests?.Add(TestType.InvocationContext);
                return Task.FromResult(request);
            };
        }

        public static ComponentInvokerDelegate<object, object> ComponentInvoker(ISet<TestType> tests)
        {
            return (request, invocationContext) =>
            {
                tests?.Add(TestType.NextInvokerInvoked);
                return Task.FromResult(request);
            };
        }
    }
}