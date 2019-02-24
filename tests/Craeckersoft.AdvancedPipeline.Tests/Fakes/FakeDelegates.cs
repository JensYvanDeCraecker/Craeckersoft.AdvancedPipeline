using System.Linq;

namespace Craeckersoft.AdvancedPipeline.Tests.Fakes
{
    public static class FakeDelegates
    {
        public static FilterDelegate<string, string> Filter { get; } = (request, invocationContext) => request != null ? new string(request.Reverse().ToArray()) : null;

        public static MiddlewareDelegate<string, string, string, string> Middleware { get; } = (request, invocationContext, next) => request != null ? new string(request.Reverse().ToArray()) : next.Invoke(request, invocationContext);

        public static ComponentDelegate<string, string, string, string> Component { get; } = next => (request, invocationContext) => request != null ? new string(request.Reverse().ToArray()) : next.Invoke(request, invocationContext);
    }
}