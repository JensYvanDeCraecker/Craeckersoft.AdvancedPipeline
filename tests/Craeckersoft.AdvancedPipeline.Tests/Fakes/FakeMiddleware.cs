using System.Linq;

namespace Craeckersoft.AdvancedPipeline.Tests.Fakes
{
    public class FakeMiddleware : IMiddleware<string, string, string, string>
    {
        public string Invoke(string request, IPipelineInvocationContext invocationContext, IComponentInvoker<string, string> next)
        {
            return request != null ? new string(request.Reverse().ToArray()) : next.Invoke(request, invocationContext);
        }
    }
}